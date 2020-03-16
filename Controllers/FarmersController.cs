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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

  
    public class FarmersController : ControllerBase
    {

        private readonly IFarmerService _farmerService;
        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _contextAccessor;
        

        private int? CommodityId { get; set; }
        private int? StateId { get; set; }


        public FarmersController(IFarmerService farmerService,
         IMapper mapper,IHttpContextAccessor contextAccessor)
        {

            _mapper = mapper;
            _farmerService = farmerService;
            _contextAccessor=contextAccessor;



        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]FarmerParams farmerParams)
        {
            


      

        ClaimsPrincipal currentUser = _contextAccessor.HttpContext.User ;  

           
         if ( currentUser.HasClaim(c => c.Type == "State"))  
        {  

         StateId = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "State").Value);  


         }  

         if(currentUser.HasClaim(c => c.Type == "Commodity")){
                     
            CommodityId = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Commodity").Value);  

         }

            var farmers = await _farmerService.GetFarmersPaginated(farmerParams,CommodityId,StateId);

            var farmersToReturn = _mapper.Map<IEnumerable<FarmerForListDTO>>(farmers);

            Response.AddPagination(farmers.CurrentPage, farmers.PageSize,
                farmers.TotalCount, farmers.TotalPages);

            return Ok(farmersToReturn);

  
            
        }

     

        // GET api/values/5

        [HttpGet("{id}")] 
        public async  Task<IActionResult> GetById(int id) 
        {
            try 
            { 
                var farmer = await  _farmerService.GetbyIdAsync(id); 
        if (farmer == null) 
        { 
            //_logger.LogError($"Owner with id: {id}, hasn't been found in db."); 
            return NotFound(); 
        } 
        else 
        { 
           // _logger.LogInfo($"Returned owner with id: {id}");
 
            var ResulttoReturn = _mapper.Map<FarmerDetailsDTO>(farmer);
            return Ok(ResulttoReturn); 
        } 
    } 
    catch (Exception ex) 
    { 
       // _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}"); 
        return StatusCode(500, "Internal server error"+ ex.ToString()); 
    } 
        }

        // POST api/values
        [HttpPost]
        
        //[FromBody]  [FromForm]
        public async Task<IActionResult> PostAsync([FromForm]FarmerUploadModel model)
        {




             

        try{


 
        if (model == null)
        {
            //_logger.LogError("Owner object sent from client is null.");
            return BadRequest("File object is null");
        }
 
        if (!ModelState.IsValid)
        {
            //_logger.LogError("Invalid owner object sent from client.");
            return BadRequest("Invalid model object");
        }

            await this._farmerService.ProcessUpload(model);
                
            
            return Ok();      


    }
    catch (Exception ex)
    {
        //_logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
        return StatusCode(500, "Internal server error" + ex.ToString());
    }



 }
        
    
           
        // PUT api/values/5
       
        [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody]StateDto statedto)
    {
        try
    {
        if (statedto == null)
	{
           // _logger.LogError("Owner object sent from client is null.");
	    return BadRequest("Owner object is null");
	}
 
	if (!ModelState.IsValid)
	{
	    //_logger.LogError("Invalid owner object sent from client.");
	    return BadRequest("Invalid model object");
	}
 
	var StateEntity = await _farmerService.GetbyIdAsync(id);
	if (StateEntity == null)
	{
	    //_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
	    return NotFound();
	}
 
	_mapper.Map(statedto, StateEntity);
 
	await _farmerService.Update(StateEntity);
 
    return Ok();

	//return NoContent();
    }
    catch (Exception ex)
    {
  	//_logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
	return StatusCode(500, "Internal server error");
    }
}

        // DELETE api/values/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        {
        try
        {
        var entity = await _farmerService.GetbyIdAsync(id);
        if (entity == null)
        {
            //_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
            return NotFound();
        }
 
        await _farmerService.Delete(entity);
 
        return Ok();
        
        //return NoContent();
    }
    catch (Exception ex)
    {
        //_logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
        return StatusCode(500, "Internal server error");
    }


            

           

    }

   
}

}