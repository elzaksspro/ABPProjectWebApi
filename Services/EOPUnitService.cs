using ProjectApi.Data;
using ProjectApi.Models;


namespace ProjectApi.Services
{
    
    public interface IEOPUnitService : IRepositoryBase<EOPUnit>
    {
        
    }

    public class EOPUnitService : RepositoryBase<EOPUnit>, IEOPUnitService
    {
        public EOPUnitService(DataContext context) : base(context)
        {
        }
        
    }
}