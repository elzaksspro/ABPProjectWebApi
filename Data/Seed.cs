using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ProjectApi.Models;
using ProjectApi.Services;
using System;


namespace DatingApp.API.Data
{
    public class Seed
    {
        public static void SeedAccessLevel(IAccessLevelService AccessLevelService)
        {
               var accessLevels = new List<AccessLevel>
                {
                    new AccessLevel{Id=1,Name = "National"},
                    new AccessLevel{Id=2, Name = "State"},
                };

 
                foreach (var accessLevel in accessLevels)
                {
                    AccessLevelService.Create(accessLevel).Wait();
                }
        }


        public static void SeedCommodities(ICommodityService commodityService)
        {
               var commodities = new List<Commodity>
                {
                    new Commodity{Id=1,Name = "Onion"},

                    new Commodity{Id=2,Name = "Tomato"},
                };

 
                foreach (var commodity in commodities)
                {
                    commodityService.Create(commodity).Wait();
                }
        }

        

        public static void SeedStates(IStateService stateService)

        {
            var Data = System.IO.File.ReadAllText("Data/StateJson.json");
            var StateTable = JsonConvert.DeserializeObject<List<State>>(Data);


            stateService.BulkInsert(StateTable).Wait();
        }

        public static void SeedLGA(ILgaService lgaService)
        {

            var Data = System.IO.File.ReadAllText("Data/LgaJson.json");
                var LgaTable = JsonConvert.DeserializeObject<List<Lga>>(Data);


            lgaService.BulkInsert(LgaTable).Wait();

               

        }


        public static void FarmersVerifcationStatus(IFarmerVarificationStatusService FarmerVarificationStatusService)
        {
               var Varificationstatus = new List<FarmerVarificationStatus>
                {
                    new FarmerVarificationStatus{Id=1,Name = "Complete"},
                    new FarmerVarificationStatus{  Id=2,Name = "Pending"},
                    new FarmerVarificationStatus{  Id=3,Name = "Processing"},
                };

 
                foreach (var status in Varificationstatus)
                {
                    FarmerVarificationStatusService.Create(status).Wait();
                }
        }
         public static void FarmOwnershipType(IFarmOwnershipTypeService FarmOwnershipTypeService)
        {
               var ownershipTypes = new List<FarmOwnershipType>
                {
                    new FarmOwnershipType{Id=1,Name = "Rent"},
                    new FarmOwnershipType{Id=2,Name = "Owner"},
                };

 
                foreach (var type in ownershipTypes)
                {
                    FarmOwnershipTypeService.Create(type).Wait();
                }
        }


        public static void Gender(IGenderService GenderService)
        {
               var genders = new List<Gender>
                {
                    new Gender{Id=1,Name = "Male"},
                    new Gender{Id=2,Name = "Female"},
                };

 
                foreach (var gender in genders)
                {
                    GenderService.Create(gender).Wait();
                }
        }

        public static void IDType(IIDTypeService IDTypeService)
        {
               var idtypes = new List<IDType>
                {
                    new IDType{Id=1,Name = "National ID"},
                    new IDType{Id=2,Name = "Voters  Card"},
                    new IDType{Id=3,Name = "Drivers Licence"},
                    new IDType{Id=4,Name = "International Passport"},
                };

 
                foreach (var type in idtypes)
                {
                    IDTypeService.Create(type).Wait();
                }
        }
      public static void MaritalStatus(IMaritalStatusService MaritalStatusService)
        {
               var maritalstatus = new List<MaritalStatus>
                {
                    new MaritalStatus{Id=1,Name = "Single"},
                    new MaritalStatus{Id=2,Name = "Married"},
                    new MaritalStatus{Id=3,Name = "Divorced"},
                    new MaritalStatus{Id=4,Name = "Widowed"},
                };

 
                foreach (var status in maritalstatus)
                {
                    MaritalStatusService.Create(status).Wait();
                }
        }

         public static void Nationality(INationalityService NationalityService)
        {
               var nationalitytypes = new List<Nationality>
                {
                    new Nationality{Id=1,Name = "Nigerian"},
                    new Nationality{Id=2,Name = "Other Nationaliyu"},
                };

 
                foreach (var type in nationalitytypes)
                {
                    NationalityService.Create(type).Wait();
                }
        }

        public static void EOPTypeSeed(IEOPTypeService EOPTypeservice)
        {
               var types = new List<EOPType>
                {
                    new EOPType{Id=1,Name = "Product"},
                    new EOPType{Id=2,Name = "Mechanization"},
                    new EOPType{Id=3,Name = "Farmer Equity Contribution"},
                };

 
                foreach (var type in types)
                {
                    EOPTypeservice.Create(type).Wait();
                }
        }

         public static void EOPUnitSeed(IEOPUnitService EOPUnitservice)
        {
               var units = new List<EOPUnit>
                {
                    new EOPUnit{Id=1,Name = "KG"},
                    new EOPUnit{Id=2,Name = "Litre"},
                    new EOPUnit{Id=3,Name = "Bags"},

                    new EOPUnit{Id=4,Name = "N/A"},
                };


 
                foreach (var unit in units)
                {
                    EOPUnitservice.Create(unit).Wait();
                }
        }


           public static void SeedEOP(IEOPService EOPservice)


        {
               var EOPS = new List<EOP>
                {
                    new EOP{  
                        ComponentName ="Arrow",
                        Brand="ARROW 1 KG",
                        Qty=1,
                        RatePerUnit=3500,
                        EOPUnitId=1,
                        EOPTypeId =1,
                        CommodityId=1

                        
                        },
                    new EOP{  
                         ComponentName ="Perfect Killer	",
                        Brand="ARROW ",
                        Qty=1,
                        RatePerUnit=3500,
                        EOPUnitId=2,
                        EOPTypeId =1,
                        CommodityId=1

                        
                        },
                };

 
                foreach (var EOP in EOPS)
                {
                    EOPservice.Create(EOP).Wait();
                }
        }

         public static void SeedInventory(IInventoryService InventoryService)
        {
               var Inventories = new List<Inventory>
                {
                    new Inventory{  
                        EOPId=1,
                        stateId=34,
                        Company ="WACOT LTD",
                        DeliveredQuantity=5000, 
                        TargetQuantity=10000, 

                        
                        
                        },
                   
                };

 
                foreach (var Invent in Inventories)
                {
                    InventoryService.Create(Invent).Wait();
                }
        }


        public static void Roles(RoleManager<Role> roleManager)
        {
               var roles = new List<Role>
                {
                    new Role{Id=1,Name = "Admin"},
                    new Role{Id=2,Name = "User"},
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }
        }

        public static void Users(UserManager<User> userManager)
        {
            
            
            
            if (!userManager.Users.Any())
            {
                //var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                //var users = JsonConvert.DeserializeObject<List<User>>(userData);



                var adminUser = new User
                {
                    UserName = "Admin",
                    Designation="Administrator",
                    accessLevelId=2,
                    stateId=34,
                    CommodityId=1,
                    LastActive=DateTime.Now,
                };

                IdentityResult result = userManager.CreateAsync(adminUser, "Admin").Result;

                if (result.Succeeded)
                {
                    var admin = userManager.FindByNameAsync("Admin").Result;
                    userManager.AddToRolesAsync(admin, new[] {"Admin", "Admin"}).Wait();
                }

                
             
            }
        }

        

        

    }
}