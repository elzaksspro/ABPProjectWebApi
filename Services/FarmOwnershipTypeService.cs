using ProjectApi.Data;
using ProjectApi.Models;


namespace ProjectApi.Services
{
    
    public interface IFarmOwnershipTypeService : IRepositoryBase<FarmOwnershipType>
    {
        
    }

    public class FarmOwnershipTypeService : RepositoryBase<FarmOwnershipType>, IFarmOwnershipTypeService
    {
        public FarmOwnershipTypeService(DataContext context) : base(context)
        {
        }
        
    }
}