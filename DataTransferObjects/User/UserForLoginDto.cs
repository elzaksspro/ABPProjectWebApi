using System.ComponentModel.DataAnnotations;

namespace ProjectApi.DataTransferObjects
{
    public class UserForLoginDto
    {
     

     [Required(ErrorMessage = "Username is required")]

        public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }
    }
    
}