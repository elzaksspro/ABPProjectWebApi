using ProjectApi.Data;
using ProjectApi.Models;


namespace ProjectApi.Services
{
    
    public interface IAccessLevelService : IRepositoryBase<AccessLevel>
    {
        
    }

    public class AccessLevelService : RepositoryBase<AccessLevel>, IAccessLevelService
    {
        public AccessLevelService(DataContext context) : base(context)
        {
        }
        
    }
}