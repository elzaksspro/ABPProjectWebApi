using ProjectApi.Data;
using ProjectApi.Models;

namespace ProjectApi.Services
{
    public interface ILgaService :IRepositoryBase<Lga>
    {
        
    }
    public class LgaService : RepositoryBase<Lga>, ILgaService
    {
        public LgaService(DataContext context) : base(context)
        {
        }
    }
}