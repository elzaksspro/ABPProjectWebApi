using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

public class FarmerUploadModel
{   

    //[Required(ErrorMessage = "Input File is required")]

    public IFormFile UploadFile { get; set; }

 
 
   

}