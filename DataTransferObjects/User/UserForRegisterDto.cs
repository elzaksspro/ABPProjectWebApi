using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ProjectApi.Models;
//using ProjectApi.DataTransferObjects.User;

namespace ProjectApi.DataTransferObjects
{
    public class UserForRegisterDto
    {
        public IFormFile Picture { get; set; }


        //[Required]
        public string username { get; set; }


        public String Roles{ get; set; }


        public int stateId { get; set; }
         public virtual  State state { get; set; }

        public int accessLevelId { get; set; }
         public virtual  AccessLevel accessLevel { get; set; }
        
          public int commodityId { get; set; }
        public virtual  Commodity commodity { get; set; }

        public string PhoneNumber { get; set; }

        public string designation { get; set; }
        

        
 


        //[Required]
        //[StringLength(24, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 24 characters")]
        public string password { get; set; }

        



        public string ProfileUrl { get; set; }




     
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}