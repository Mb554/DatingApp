using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<PhotoApprovedDto>> GetNotApprovedPhotos();
        Task<Photo> GetPhotoById(int id);
        void DeletePhoto (Photo photo);
    }
}