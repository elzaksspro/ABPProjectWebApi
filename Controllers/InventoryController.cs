using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Models;

using ProjectApi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IO;
using Microsoft.AspNetCore.Http;


using System.Text;
using CsvHelper;
using System.Globalization;
using ProjectApi.Params;
using ProjectApi.DataTransferObjects;
using ProjectApi.Helpers;

using Microsoft.AspNetCore.Http;


namespace ProjectApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]

  
    public class InventoryController : ControllerBase
    {

        private readonly IInventoryService _InventoryService;
        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _contextAccessor;
        

        private int? CommodityId { get; set; }
        private int? StateId { get; set; }


        public InventoryController(IInventoryService InventoryService,
         IMapper mapper,IHttpContextAccessor contextAccessor)
        {

            _mapper = mapper;
            _InventoryService = InventoryService;
            _contextAccessor=contextAccessor;



        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]InventoryParams Params)
        {
            


      

        ClaimsPrincipal currentUser = _contextAccessor.HttpContext.User ;  

           
         if ( currentUser.HasClaim(c => c.Type == "State"))  
        {  

         StateId = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "State").Value);  


         }  

         if(currentUser.HasClaim(c => c.Type == "Commodity")){
                     
            CommodityId = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Commodity").Value);  

         }

            var entities = await _InventoryService.GetPaginated(Params,CommodityId,StateId);

            var entitiesToReturn = _mapper.Map<IEnumerable<InventoryForListDTO>>(entities);

            Response.AddPagination(entities.CurrentPage, entities.PageSize,
                entities.TotalCount, entities.TotalPages);

            return Ok(entitiesToReturn);

  
            
        }

     

        // GET api/values/5

        [HttpGet("{id}")] 
        public async  Task<IActionResult> GetById(int id) 
        {
            try 
            { 
                var entity = await  _InventoryService.GetbyIdAsync(id); 
        if (entity == null) 
        { 
            //_logger.LogError($"Owner with id: {id}, hasn't been found in db."); 
            return NotFound(); 
        } 
        else 
        { 
           // _logger.LogInfo($"Returned owner with id: {id}");
 
            var ResulttoReturn = _mapper.Map<EOPForListDTO>(entity);
            return Ok(ResulttoReturn); 
        } 
    } 
    catch (Exception ex) 
    { 
       // _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}"); 
        return StatusCode(500, "Internal server error"+ ex.ToString()); 
    } 
        }

      
         
}

}