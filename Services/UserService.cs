using System.Reflection.Metadata.Ecma335;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Data;
using ProjectApi.Models;
using ProjectApi.Params;

using System.Collections.Generic;
using System.IO;



using ProjectApi.DataTransferObjects;

namespace ProjectApi.Services
{

    public interface IUserService
    {

    Task<bool> UserExists(string username);
    Task<User> GetUser(int id);
    String ProcessUpload(UserForRegisterDto model);


     //Task<PagedList<User>> GetUsers(UserParams userParams,int? userCommodityId, int? StateId);
     //Task<List<User>> GetUsers(UserParams userParams,int? userCommodityId, int? StateId);


    Task<bool> SaveAll();


    }
    
    public class UserService : IUserService
    {

        string fullPath;

        
     

         private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == username))
                return true;

            return false;
        }

          public async Task<User> GetUser(int id)
        {
            var query = _context.Users.AsQueryable();

            

            var user = await query.FirstOrDefaultAsync(u => u.Id == id);

         
                                  

            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }



          
        public String ProcessUpload(UserForRegisterDto model)
        {

            var folderName = Path.Combine("Resources", "UserProfile");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

 

    // Saving Image on Server
    try{
         var uploadedfile = model.Picture;
        string fileExtension = Path.GetExtension(uploadedfile.FileName);

        if (uploadedfile.Length > 0) 
        {

           

            //var fileName=uploadedfile.FileName;
       
            var fileName=Guid.NewGuid().ToString();
            var filenameforSave=fileName+"."+fileExtension;
             this.fullPath = Path.Combine(pathToSave, filenameforSave);
            //var dbPath = Path.Combine(folderName, fileName);
 
            using (var stream = new FileStream(this.fullPath, FileMode.Create))
            {
                uploadedfile.CopyTo(stream);
            }

        }
    

    }catch(Exception e)
    {
        
        Console.WriteLine(e);
    
    }

    return  this.fullPath;
}

    }

}