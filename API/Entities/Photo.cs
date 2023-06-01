using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id{get;set;}
        public String Url {get;set;}
        public bool IsMain {get;set;}
        public string PublicId {get;set;}

        public bool isApproved {get; set;}

        public int AppUserId {get;set;}
        public AppUser AppUser{get;set;}

    }
}