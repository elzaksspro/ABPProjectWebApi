using ProjectApi.Data;
using ProjectApi.Models;


namespace ProjectApi.Services
{
    
    public interface IEOPTypeService : IRepositoryBase<EOPType>
    {
        
    }

    public class EOPTypeService : RepositoryBase<EOPType>, IEOPTypeService
    {
        public EOPTypeService(DataContext context) : base(context)
        {
        }
        
    }
}