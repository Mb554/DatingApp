using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _dataContext;
        public PhotoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            
        }
        public void DeletePhoto(Photo photo)
        {
            _dataContext.Photos.Remove(photo);
        }

        public async Task<IEnumerable<PhotoApprovedDto>> GetNotApprovedPhotos()
        {
           return await _dataContext.Photos.IgnoreQueryFilters().Where(p=> p.isApproved == false).Select(k => new PhotoApprovedDto{
            Id = k.Id, 
            Username = k.AppUser.UserName,
            Url = k.Url,
            IsApproved = k.isApproved
           }).ToListAsync();
        }

        public async Task<Photo> GetPhotoById(int id)
        {
           return await _dataContext.Photos.IgnoreQueryFilters().SingleOrDefaultAsync(p =>p.Id== id);
        }
    }
}