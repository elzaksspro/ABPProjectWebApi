using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ProjectApi.Models
{
  
 public class User : IdentityUser<int>
    {

        public string Designation { get; set; }

        public string ProfileUrl{ get; set; }

        public int? stateId { get; set; }
        public virtual  State state { get; set; }

        public int CommodityId { get; set; }
        public virtual  Commodity commodity { get; set; }

        public int accessLevelId { get; set; }
         public virtual  AccessLevel accessLevel { get; set; }
        public DateTime Created { get; set; }
        
        public DateTime LastActive { get; set; }




        public virtual ICollection<UserRole> UserRoles { get; set; }



       

    
    }
}