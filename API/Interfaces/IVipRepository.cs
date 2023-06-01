using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IVipRepository
    {
        Task<VIPVisits> GetVisitor(int SourceUserId, int TargetUserId);
        Task<AppUser> GetVisitorWithViews(int userId);
        Task<PagedList<VipDto>> GetUserVisits(VipParams vipParams);
        Task GetUserVisits(int visiterId);
    }
}