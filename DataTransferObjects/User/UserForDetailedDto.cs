using System;
using System.Collections.Generic;
using ProjectApi.Models;

namespace ProjectApi.DataTransferObjects
{
    public class UserForDetailedDto
    {

        public string Designation { get; set; }
        
        public string userName { get; set; }


        public string ProfileUrl{ get; set; }



        
     

        public int stateId { get; set; }
         public virtual  State state { get; set; }

        public int accessLevelId { get; set; }
         public virtual  AccessLevel accessLevel { get; set; }


       
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

       
    }
}