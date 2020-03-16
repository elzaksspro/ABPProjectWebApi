using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectApi.DataTransferObjects;
using ProjectApi.Models;

    [AllowAnonymous]

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;


        public AuthController(IConfiguration config,IMapper mapper,SignInManager<User> signInManager,UserManager<User> userManager)
        {
            _userManager = userManager;

            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
        }

   

        [HttpPost]
        public async Task<IActionResult> Post(UserForLoginDto userForLoginDto)
        {

        try{

        if (userForLoginDto == null || !ModelState.IsValid)
        {
            //_logger.LogError("Owner object sent from client is null.");
            return BadRequest(" Invalid model object");
        }
 
        else {
        
        var user = await _userManager.FindByNameAsync(userForLoginDto.UserName);
        


        var result = await _signInManager
                .CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

        if (result.Succeeded)
            {
                var appUser = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.NormalizedUserName == userForLoginDto.UserName.ToUpper());

                var userToReturn = _mapper.Map<UserForDetailedDto>(appUser);

                //UserManager.ClaimsIdentityFactory = new CustomUserClaimsPrincipalFactory() { };


                return Ok(new
                {
                    token = GenerateJwtToken(appUser).Result,
                    user = userToReturn
                });
            }

            else{

                    return Unauthorized();
            }

        }
    }
    catch (Exception ex)
    {
        //_logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
        return StatusCode(500, "Internal server error" + ex.ToString());
    }

     }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Commodity",  user.CommodityId.ToString()),
                new Claim("State", user.stateId.ToString()),

                


                
  

            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Key").Value));

            //var Issuer = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Issuer").Value));

            var IssuerParty = _config.GetSection("AppSettings:Issuer").Value;
            var AudienceParty = _config.GetSection("AppSettings:Issuer").Value;

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

 //                TokenIssuerName=Issuer


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer=IssuerParty,
                Audience=AudienceParty,

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }



        
    }