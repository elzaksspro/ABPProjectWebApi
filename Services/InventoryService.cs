using ProjectApi.Data;
using ProjectApi.Models;
using ProjectApi.Params;
using System.Threading.Tasks;
using ProjectApi.Data;
using ProjectApi.Models;
using ProjectApi.Params;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using ProjectApi.Data;
using ProjectApi.Models;
using ProjectApi.Params;

namespace ProjectApi.Services
{
    
    public interface IInventoryService : IRepositoryBase<Inventory>
    {
        
     Task<PagedList<Inventory>> GetPaginated(InventoryParams Params, int? userCommodityId,  int? StateId);


    }

    public class InventoryService : RepositoryBase<Inventory>, IInventoryService
    {
             private readonly DataContext Context;

        public InventoryService(DataContext context) : base(context)
        {
                Context = context;

            
        } 

        public async Task<PagedList<Inventory>> GetPaginated(InventoryParams Params,int? userCommodityId, int? StateId)
        {

            
            var entities = from f in  Context.Inventories select f;
            entities = StateId == null ? entities : entities.Where(c => c.stateId == StateId);

            entities = userCommodityId == null ? entities : entities.Where(c => c.EOP.CommodityId == userCommodityId);
            entities=entities.OrderByDescending(u => u.Id).AsQueryable();
            
           

            return await PagedList<Inventory>.CreateAsync(entities, Params.PageNumber, Params.PageSize);
        }
        
    }
}