using ProjectApi.Data;
using ProjectApi.Models;


namespace ProjectApi.Services
{
    
    public interface IMaritalStatusService :IRepositoryBase<MaritalStatus>
    {
        
    }

    public class MaritalStatusService : RepositoryBase<MaritalStatus>, IMaritalStatusService
    {
        public MaritalStatusService(DataContext context) : base(context)
        {
        }
        
    }
}