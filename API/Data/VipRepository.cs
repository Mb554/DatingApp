using API.DTOs;
using API.Entities;
using API.Extenstions;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class VipRepository: IVipRepository
    {
        private readonly DataContext _context;
        public VipRepository(DataContext context)
        {
            _context = context;
            
        }

        public async Task<VIPVisits> GetVisitor(int SourceUserId, int TargetUserId)
        {
           return await _context.Visits.FindAsync(SourceUserId,TargetUserId);
        }

        public async Task<AppUser> GetVisitorWithViews(int userId)
        {
            return await _context.Users.Include(x => x.VisitedUser).FirstOrDefaultAsync( v => v.Id == userId);
        }

        public async Task<PagedList<VipDto>> GetUserVisits(VipParams vipParams)
        {
            var users = _context.Users.OrderBy(v => v.UserName).AsQueryable();
            var visits = _context.Visits.AsQueryable();

            if(vipParams.Predicate == "Viewed"){
                visits = visits.Where(visit => visit.VisiterId == vipParams.UserId);
                users = visits.Select(visit => visit.Visited);
            }

            if(vipParams.Predicate == "viewBy")
            {
                visits = visits.Where(visit => visit.TargetUserId == vipParams.UserId);
                users = visits.Select(visit => visit.Visiter);
            }

            var viewedUser = users.Select(user => new VipDto{
                UserName = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault( p => p.IsMain).Url,
                City = user.City,
                Id =user.Id
            });

            return await PagedList<VipDto>.CreateAsync(viewedUser, vipParams.PageNumber,vipParams.PageSize);
        }

        public Task GetUserVisits(int visiterId)
        {
            throw new NotImplementedException();
        }
    }
}