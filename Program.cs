using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProjectApi.Data;
using ProjectApi.Models;
using ProjectApi.Services;

namespace ProjectApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>();

                    var AccessLevelService = services.GetRequiredService<IAccessLevelService>();
                    var CommodityService = services.GetRequiredService<ICommodityService>();

                    var FarmerVarificationStatusService = services.GetRequiredService<IFarmerVarificationStatusService>();
                    var FarmOwnershipTypeService = services.GetRequiredService<IFarmOwnershipTypeService>();
                    var GenderService = services.GetRequiredService<IGenderService>();
                     var IDTypeService = services.GetRequiredService<IIDTypeService>();
                    var LgaService = services.GetRequiredService<ILgaService>();

                    var MaritalStatusService = services.GetRequiredService<IMaritalStatusService>();
                    var NationalityService = services.GetRequiredService<INationalityService>();
                    var roleManager = services.GetRequiredService<RoleManager<Role>>();
                    var StateService = services.GetRequiredService<IStateService>();

                    var userManager = services.GetRequiredService<UserManager<User>>();

                    var EOPservice = services.GetRequiredService<IEOPService>();
                    var EOPTypeservice = services.GetRequiredService<IEOPTypeService>();
                    var EOPUnitservice = services.GetRequiredService<IEOPUnitService>();

                    var InventoryService = services.GetRequiredService<IInventoryService>();



                    context.Database.Migrate();
                    context.Database.EnsureCreated();


                    if (!userManager.Users.Any()){

                            Seed.SeedAccessLevel(AccessLevelService);
                            Seed.SeedCommodities(CommodityService);

                            Seed.SeedStates(StateService);
                            Seed.SeedLGA(LgaService);

                            
                            Seed.FarmersVerifcationStatus(FarmerVarificationStatusService);
                            Seed.FarmOwnershipType(FarmOwnershipTypeService);
                            Seed.Gender(GenderService);
                            Seed.IDType(IDTypeService);
                            Seed.MaritalStatus(MaritalStatusService);
                            Seed.Nationality(NationalityService);
                            Seed.EOPTypeSeed(EOPTypeservice);
                            Seed.EOPUnitSeed(EOPUnitservice);
                            Seed.SeedEOP(EOPservice);

                            Seed.SeedInventory(InventoryService);

                            
                            Seed.Roles(roleManager);
                            Seed.Users(userManager);

                    }
                    
                   

                    
                } 
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }

            host.Run();

            //CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
