using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Models;
using ProjectApi.DataTransferObjects;

using ProjectApi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ProjectApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]

  
    public class AccesslevelController : ControllerBase
    {

        private readonly IAccessLevelService _accesslevelService;
        private readonly IMapper _mapper;


        public AccesslevelController(IAccessLevelService accesslevelService, IMapper mapper)
        {

            _mapper = mapper;
            _accesslevelService = accesslevelService;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
        
            var accesslist = await _accesslevelService.GetAll();

            if (accesslist == null)
                return NotFound();

            return Ok(accesslist);    
            
        }

     

        // GET api/values/5

        [HttpGet("{id}")] 
        public async  Task<IActionResult> GetById(int id) 
        {
            try 
            { 
                var access = await  _accesslevelService.GetbyIdAsync(id); 
        if (access == null) 
        { 
            //_logger.LogError($"Owner with id: {id}, hasn't been found in db."); 
            return NotFound(); 
        } 
        else 
        { 
           // _logger.LogInfo($"Returned owner with id: {id}");
 
            var ResulttoReturn = _mapper.Map<AccessLevelDto>(access);
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
        public IActionResult  Post([FromBody] AccessLevelForCreationDto accesslevel)
        {
        try{

        if (accesslevel == null)
        {
            //_logger.LogError("Owner object sent from client is null.");
            return BadRequest("Owner object is null");
        }
 
        if (!ModelState.IsValid)
        {
            //_logger.LogError("Invalid owner object sent from client.");
            return BadRequest("Invalid model object");
        }
 
        var Entity = _mapper.Map<AccessLevel>(accesslevel);
 
        _accesslevelService.Create(Entity);
 
        var EntityToReturn = _mapper.Map<AccessLevelDto>(Entity);
 
        return CreatedAtRoute("GetById", new { id = EntityToReturn.Id },EntityToReturn);
    }
    catch (Exception ex)
    {
        //_logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
        return StatusCode(500, "Internal server error" + ex.ToString());
    }

 }
        
           
        // PUT api/values/5
       
        [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody]AccessLevelDto accessdto)
    {
        try
    {
        if (accessdto == null)
	{
           // _logger.LogError("Owner object sent from client is null.");
	    return BadRequest("Owner object is null");
	}
 
	if (!ModelState.IsValid)
	{
	    //_logger.LogError("Invalid owner object sent from client.");
	    return BadRequest("Invalid model object");
	}
 
	var Entity = await _accesslevelService.GetbyIdAsync(accessdto.Id);
	if (Entity == null)
	{
	    //_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
	    return NotFound();
	}
 
	_mapper.Map(accessdto, Entity);
 
	await _accesslevelService.Update(Entity);
 
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
        var entity = await _accesslevelService.GetbyIdAsync(id);
        if (entity == null)
        {
            //_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
            return NotFound();
        }
 
        await _accesslevelService.Delete(entity);
 
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