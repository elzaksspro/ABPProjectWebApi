using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Data;
using ProjectApi.DataTransferObjects;
using ProjectApi.Models;

namespace ProjectApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]


    public class RolesController : ControllerBase
    {
        private RoleManager<Role> _roleManager ;
       // RoleManager<IdentityRole> roleManager;

        private readonly DataContext _context;

        public RolesController(RoleManager<Role> roleManager,DataContext context)
        {
            _roleManager = roleManager;
            _context = context;

        }
 

        [HttpGet]
        public async Task<IActionResult> Get()
        {
        
            //var list =  _roleManager.Roles;

             var Roles = from roles in _context.Roles  select new
                                  { Id=roles.Id,
                                  name= roles.Name,
                                     
                                      };

                                               

            //if (list == null)
             //   return NotFound();

            return Ok(Roles);    
            
        }
 
        [HttpPost]
        public async Task<IActionResult> Create(RoleForCreationDTO Model)
        {
        try{

        if (Model == null)
        {
            //_logger.LogError("Owner object sent from client is null.");
            return BadRequest("Role object is null");
        }
 
        if (!ModelState.IsValid)
        {
            //_logger.LogError("Invalid owner object sent from client.");
            return BadRequest("Invalid model object");
        }

        var result = await _roleManager.CreateAsync(new  Role{Name = Model.Name});                   
        
        if (result.Succeeded)
        {
            return Ok();     
        
        }else{

            return StatusCode(500, "Internal server error" + result.Errors.ToString());
            

        }
 
 
 
    }
    catch (Exception ex)
    {
        //_logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
        return StatusCode(500, "Internal server error" + ex.ToString());
    }

}
 
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded){
                    
                        return Ok();
                }
                else
                {

                    return StatusCode(500, "Internal server error" + result.Errors.ToString());

                }
            }
            else 
            {
                
                return NotFound();
            }

              
        }
 
       
    }
}