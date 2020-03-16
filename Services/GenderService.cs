using ProjectApi.Data;
using ProjectApi.Models;


namespace ProjectApi.Services
{
    
    public interface IGenderService  : IRepositoryBase<Gender>
    {
        
    }

    public class GenderService : RepositoryBase<Gender>, IGenderService
    {
        public GenderService(DataContext context) : base(context)
        {
        }
        
    }
}