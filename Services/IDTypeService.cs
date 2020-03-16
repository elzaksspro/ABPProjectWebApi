using ProjectApi.Data;
using ProjectApi.Models;

namespace ProjectApi.Services
{
    public interface IIDTypeService : IRepositoryBase<IDType>
    {
        
    }
    public class IDTypeService : RepositoryBase<IDType>, IIDTypeService
    {
        public IDTypeService(DataContext context) : base(context)
        {
        }
    }
}