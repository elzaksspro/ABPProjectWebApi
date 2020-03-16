using System.Threading.Tasks;
using ProjectApi.Data;
using ProjectApi.Models;
using ProjectApi.Params;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;


namespace ProjectApi.Services
{
    
    public interface IEOPService : IRepositoryBase<EOP>
    {

        Task<PagedList<EOP>> GetPaginated(EOPParams eOPParams, int? userCommodityId,  int? StateId);

        
    }

    public class EOPService : RepositoryBase<EOP>, IEOPService
    {
        private readonly DataContext Context;

        public EOPService(DataContext _context) : base(_context)
        {
                    Context = _context;

        }

            public async Task<PagedList<EOP>> GetPaginated(EOPParams eOPParams,int? userCommodityId, int? StateId)
        {

            
            var entities = from f in  Context.EOPs select f;
            entities = userCommodityId == null ? entities : entities.Where(c => c.CommodityId == userCommodityId);
            entities=entities.OrderByDescending(u => u.Id).AsQueryable();
            
           

            return await PagedList<EOP>.CreateAsync(entities, eOPParams.PageNumber, eOPParams.PageSize);
        }
    
        
    }
}