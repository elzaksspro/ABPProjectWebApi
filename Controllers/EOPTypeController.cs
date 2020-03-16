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

  
    public class EoptypeController : ControllerBase
    {

        private readonly IEOPTypeService _eOPTypeService;
        private readonly IMapper _mapper;


        public EoptypeController(IEOPTypeService eOPTypeService, IMapper mapper)
        {

            _mapper = mapper;
            _eOPTypeService = eOPTypeService;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
        
            var list = await _eOPTypeService.GetAll();

            if (list == null)
                return NotFound();

            return Ok(list);    
            
        }

     

        // GET api/values/5

        [HttpGet("{id}")] 
        public async  Task<IActionResult> GetById(int id) 
        {
            try 
            { 
                var model = await  _eOPTypeService.GetbyIdAsync(id); 
        if (model == null) 
        { 
            //_logger.LogError($"Owner with id: {id}, hasn't been found in db."); 
            return NotFound(); 
        } 
        else 
        { 
           // _logger.LogInfo($"Returned owner with id: {id}");
 
            var ResulttoReturn = _mapper.Map<EOPTypeDto>(model);
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
        public IActionResult  Post([FromBody] EOPTypeCreationDTO EOPType)
        {
        try{

        if (EOPType == null)
        {
            //_logger.LogError("Owner object sent from client is null.");
            return BadRequest("Owner object is null");
        }
 
        if (!ModelState.IsValid)
        {
            //_logger.LogError("Invalid owner object sent from client.");
            return BadRequest("Invalid model object");
        }
 
        var Entity = _mapper.Map<EOPType>(EOPType);
 
        _eOPTypeService.Create(Entity);
 
        var EntityToReturn = _mapper.Map<EOPTypeDto>(Entity);
 
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
    public async Task<IActionResult> Put(int id, [FromBody]EOPTypeDto model)
    {
        try
    {
        if (model == null)
	{
           // _logger.LogError("Owner object sent from client is null.");
	    return BadRequest("Owner object is null");
	}
 
	if (!ModelState.IsValid)
	{
	    //_logger.LogError("Invalid owner object sent from client.");
	    return BadRequest("Invalid model object");
	}
 
	var Entity = await _eOPTypeService.GetbyIdAsync(model.Id);
	if (Entity == null)
	{
	    //_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
	    return NotFound();
	}
 
	_mapper.Map(model, Entity);
 
	await _eOPTypeService.Update(Entity);
 
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
        var entity = await _eOPTypeService.GetbyIdAsync(id);
        if (entity == null)
        {
            //_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
            return NotFound();
        }
 
        await _eOPTypeService.Delete(entity);
 
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