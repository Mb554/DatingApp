namespace API.Entities
{
    public class VIPVisits
    {
       public AppUser Visiter { get; set; } 
       public int VisiterId { get; set; }
       public AppUser Visited {get;set;}
       public int TargetUserId{get;set;}
    }
}