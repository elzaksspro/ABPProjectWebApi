using System;
using System.Collections.Generic;
using ProjectApi.Models;
namespace ProjectApi.DataTransferObjects
{
    public class UserForListDto
    {

         public string UserName { get; set; }

         public string Designation { get; set; }

        public string ProfileUrl{ get; set; }

        public int? stateId { get; set; }
        public virtual  State state { get; set; }

        public int commodityId { get; set; }
        public virtual  Commodity commodity { get; set; }

        public int accessLevelId { get; set; }
         public virtual  AccessLevel accessLevel { get; set; }
     


        public virtual ICollection<UserRole> UserRoles { get; set; }

    
    }
}