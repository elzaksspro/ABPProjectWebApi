using ProjectApi.Data;
using ProjectApi.Models;


namespace ProjectApi.Services
{
    
    public interface IFarmerVarificationStatusService : IRepositoryBase<FarmerVarificationStatus>
    {
        
    }

    public class FarmerVarificationStatusService : RepositoryBase<FarmerVarificationStatus>, IFarmerVarificationStatusService
    {
        public FarmerVarificationStatusService(DataContext context) : base(context)
        {
        }
        
    }
}