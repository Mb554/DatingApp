using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace API.Controllers
{
    public class VIPController : BaseApiController
    {
        private readonly IVipRepository _vipRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;
       public VIPController(IUserRepository userRepository, IVipRepository vipRepository, IUnitOfWork uow)
       {
            _uow = uow;
            _userRepository = userRepository;
            _vipRepository = vipRepository;
        
       }

       [HttpPost("{username}")]
       public async Task<ActionResult> VisitVip(string username){
        var VisiterId = User.GetUserId();
        var viewedUser = await _userRepository.GetUserByUsernameAsync(username);
        var visitUser = await _vipRepository.GetVisitorWithViews(VisiterId);
        

        if(viewedUser == null) return NotFound();
        if(visitUser.UserName == username) return BadRequest("Cannot visit yourself");

        var userVisit = await _vipRepository.GetUserVisits(VisiterId, visitUser.Id);

        if(userVisit != null) return BadRequest("You already visited the user");
        userVisit= new VIPVisits{
            VisiterId = VisiterId,
            Visiter = visitUser.Id
        };
        
        visitUser.viewedUser.Add(userVisit);
        
        if(await _uow.Complete()) return Ok();
        return BadRequest("Failed to view visitor");


        
       }
    
    }
}