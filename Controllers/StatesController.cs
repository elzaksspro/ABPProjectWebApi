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

  
    public class StatesController : ControllerBase
    {

        private readonly IStateService _stateService;
        private readonly IMapper _mapper;


        public StatesController(IStateService stateService, IMapper mapper)
        {

            _mapper = mapper;
            _stateService = stateService;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
        
            var stateslist = await _stateService.GetAll();

            if (stateslist == null)
                return NotFound();

            return Ok(stateslist);    
            
        }

     

        // GET api/values/5

        [HttpGet("{id}")] 
        public async  Task<IActionResult> GetById(int id) 
        {
            try 
            { 
                var entity = await  _stateService.GetbyIdAsync(id); 
        if (entity == null) 
        { 
            //_logger.LogError($"Owner with id: {id}, hasn't been found in db."); 
            return NotFound(); 
        } 
        else 
        { 
           // _logger.LogInfo($"Returned owner with id: {id}");
 
            var ResulttoReturn = _mapper.Map<StateDto>(entity);
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
        public IActionResult  Post([FromBody] StateForCreationDto state)
        {
        try{

        if (state == null)
        {
            //_logger.LogError("Owner object sent from client is null.");
            return BadRequest("Owner object is null");
        }
 
        if (!ModelState.IsValid)
        {
            //_logger.LogError("Invalid owner object sent from client.");
            return BadRequest("Invalid model object");
        }
 
        var Entity = _mapper.Map<State>(state);
 
        _stateService.Create(Entity);
 
        var EntityToReturn = _mapper.Map<StateDto>(Entity);
 
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
 
	var StateEntity = await _stateService.GetbyIdAsync(statedto.Id);
	if (StateEntity == null)
	{
	    //_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
	    return NotFound();
	}
 
	_mapper.Map(statedto, StateEntity);
 
	await _stateService.Update(StateEntity);
 
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
        var entity = await _stateService.GetbyIdAsync(id);
        if (entity == null)
        {
            //_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
            return NotFound();
        }
 
        await _stateService.Delete(entity);
 
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