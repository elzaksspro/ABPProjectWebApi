using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.DataTransferObjects;
using ProjectApi.Helpers;
using ProjectApi.Models;
using ProjectApi.Params;
using ProjectApi.Services;
using System.Security.AccessControl;
using System.Linq;
using System.IO;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
//using ProjectApi.DataTransferObjects.User;


using Microsoft.AspNetCore.Http;
using ProjectApi.Data;
using Microsoft.EntityFrameworkCore;

[AllowAnonymous]
//[ServiceFilter(typeof(LogUserActivity))]

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly DataContext _context;

        

        private int? CommodityIdClaim { get; set; }
        private int? StateIdClaim { get; set; }

        public UsersController(IUserService userService, IMapper mapper,UserManager<User> userManager,
        IHttpContextAccessor contextAccessor,DataContext context)
        {
            _userManager = userManager;
            _contextAccessor=contextAccessor;
            _mapper = mapper;
            _userService = userService;

            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {

             ClaimsPrincipal currentUser = _contextAccessor.HttpContext.User ;  

           
         if ( currentUser.HasClaim(c => c.Type == "State"))  
        {  

         StateIdClaim = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "State").Value);  


         }  

         if(currentUser.HasClaim(c => c.Type == "Commodity")){
                     
            CommodityIdClaim = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Commodity").Value);  

         }


            var userList = await (from user in _context.Users
                                  orderby user.UserName

                                  select new
                                  {
                                      Id = user.Id,
                                     UserName = user.UserName,
                                     state=user.state,
                                     commodity=user.commodity,
                                     accessLevel=user.accessLevel,

                                      CommodityId=user.CommodityId,
                                      Designation=user.Designation,
                                      ProfileUrl=user.ProfileUrl,
                                      stateId=user.stateId,
                                      accessLevelId=user.accessLevelId,



                                      Roles = (from userRole in user.UserRoles
                                               join role in _context.Roles
                                               on userRole.RoleId
                                               equals role.Id
                                               select role.Name).ToList()
                                  }).ToListAsync();

                                  

                   // userList = CommodityIdClaim == null ? userList : userList.Where(c => c.CommodityId == CommodityIdClaim);
            //userList = StateIdClaim == null ? userList : userList.Where(c => c.stateId == StateIdClaim);
            //userList=userList.OrderByDescending(u => u.Id).ToListAsync();
            
            //.AsQueryable();
        
                                  
            return Ok(userList);
        
  
       
      
        
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            
            var user = await _userService.GetUser(id);

            //var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(user);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _userService.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _userService.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] UserForRegisterDto userForRegisterDto)
        {


            




           var userToCreate = _mapper.Map<User>(userForRegisterDto);
           
           userToCreate.ProfileUrl= this._userService.ProcessUpload(userForRegisterDto);

      
            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.password);

            var userToReturn = _mapper.Map<UserForDetailedDto>(userToCreate);

            if (result.Succeeded)
            {
                
                //var selectedRoles = userForRegisterDto.Roles;

                var selectedRoles = JsonConvert.DeserializeObject<List<string>>(userForRegisterDto.Roles);

        
                var userFromDb = _userManager.FindByNameAsync(userForRegisterDto.username).Result;

                
                //IEnumerable<string> Roles = new string[] {selectedRoles};



                 foreach (var role in selectedRoles)
                {

                  _userManager.AddToRolesAsync(userFromDb, selectedRoles).Wait();             

                    Console.WriteLine(role);

                    //IEnumerable<string> RolesToAdd =role.Cast<string>();
                  //_userManager.AddToRolesAsync(userFromDb, RolesToAdd).Wait();             
                  
                }
                


                
                


                //

                return CreatedAtRoute("GetUser", 
                   new { controller = "Users", id = userToCreate.Id }, userToReturn);
            }

            return BadRequest(result.Errors);

 
        }
    }