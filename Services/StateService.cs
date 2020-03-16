using ProjectApi.Data;
using ProjectApi.Models;

namespace ProjectApi.Services
{
    public interface IStateService : IRepositoryBase<State>
    {
        
    }
    public class StateService : RepositoryBase<State>, IStateService
    {
        public StateService(DataContext context) : base(context)
        {
        }
    }
}