using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ProjectApi.Models;

namespace ProjectApi.Helpers
{
public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User> 
{     
    public CustomUserClaimsPrincipalFactory(UserManager<User> userManager,
    IOptions<IdentityOptions> optionsAccessor): base(userManager, optionsAccessor)     
    {
    }
      
     
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {


         var identity = await base.GenerateClaimsAsync(user);
         identity.AddClaim(new Claim("Commodity", user.CommodityId.ToString()));         

         identity.AddClaim(new Claim("State",  user.stateId.ToString()));         

         return identity;


            
    } 

        
        
}

}