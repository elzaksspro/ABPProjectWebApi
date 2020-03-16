using ProjectApi.Data;
using ProjectApi.Models;
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
    
    public interface ICommodityService : IRepositoryBase<Commodity>
    {
        Task<PagedList<Commodity>> GetPaginated(CommodityParam commodityParam, int? userCommodityId,  int? StateId);

        
    }

    public class CommodityService : RepositoryBase<Commodity>, ICommodityService
    {
        private readonly DataContext Context;

        public CommodityService(DataContext _context) : base(_context)
        {
            Context = _context;

        }

         public async Task<PagedList<Commodity>> GetPaginated(CommodityParam commodityParam,int? userCommodityId, int? StateId)
        {

            
            var entities = from f in  Context.Commodities select f;
            entities=entities.OrderByDescending(u => u.Id).AsQueryable();
            
           

            return await PagedList<Commodity>.CreateAsync(entities, commodityParam.PageNumber, commodityParam.PageSize);
        }
        
    }
}