using ProjectApi.Data;
using ProjectApi.Models;


namespace ProjectApi.Services
{
    
    public interface INationalityService :IRepositoryBase<Nationality>
    {
        
    }

    public class NationalityService : RepositoryBase<Nationality>, INationalityService
    {
        public NationalityService(DataContext context) : base(context)
        {
        }
        
    }
}