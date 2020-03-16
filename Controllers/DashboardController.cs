using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Models;
using ProjectApi.Data;

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
    public class DashboardController: ControllerBase
    {

        private readonly DataContext Context;
        public DashboardController(DataContext _context) 
        {
                    Context = _context;

        }

          [HttpGet("GetStat")]
        public  IActionResult GetStat()
        {
            DashboardStat dashboardStat = new DashboardStat();
            
            dashboardStat.TotalFarmersCount=Context.Farmers.Count();
            dashboardStat.MappedFarmersCount=Context.Farmers.Where(c=>c.MappingStatus==1).Count();
            dashboardStat.DisburesedFarmersCount=Context.Farmers.Where(c=>c.DisbursmentStatus==1).Count();

            return Ok(dashboardStat);
        }

        [HttpGet("GetDistribution")]
        public  IActionResult GetDistribution()
        {

        var grouped = from b in Context.Farmers.Where(c=>c.DisbursmentStatus==1)
              group b.Id by b.stateofresidence.Name into g
              select new
              {
                  Label = g.Key,
                  Data = g.Count()
              };


            var result = grouped.ToList();

            return Ok(result);

 
        }



    
        
    }
}