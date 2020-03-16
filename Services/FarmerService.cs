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

    public interface IFarmerService : IRepositoryBase<Farmer>
    {
        Task DeleteFile(string Path );


        Task ProcessUpload(FarmerUploadModel model);

        Task<PagedList<Farmer>> GetFarmersPaginated(FarmerParams farmerParams, int? userCommodityId,  int? StateId);



        
    }
    public class FarmerService : RepositoryBase<Farmer>, IFarmerService
    {
        private readonly DataContext Context;

       

        string fullPath;

        public FarmerService(DataContext _context) : base(_context)
        {
          
        Context = _context;
         

        }

           public async Task<PagedList<Farmer>> GetFarmersPaginated(FarmerParams farmerParams,int? userCommodityId, int? StateId)
        {
            var farmers = from f in Context.Farmers  select f;
            farmers = userCommodityId == null ? farmers : farmers.Where(c => c.CommodityId == userCommodityId);
            farmers = StateId == null ? farmers : farmers.Where(c => c.stateofresidenceId == StateId);
            farmers = farmerParams.BVN == null ? farmers : farmers.Where(c => c.BVN == farmerParams.BVN);

            farmers=farmers.OrderByDescending(u => u.Id).AsQueryable();
            
            //ToList();
            
           // farmers=farmers.AsQueryable();
            //AsQueryable();

            return await PagedList<Farmer>.CreateAsync(farmers, farmerParams.PageNumber, farmerParams.PageSize);
        }
    




        public Task ProcessUpload(FarmerUploadModel model)
        {

            var folderName = Path.Combine("Resources", "FarmersList");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
 

    // Saving Image on Server
    try{
         var uploadedfile = model.UploadFile;

        if (uploadedfile.Length > 0) 
        {

           

            var fileName=uploadedfile.FileName;
       
             this.fullPath = Path.Combine(pathToSave, fileName);
            //var dbPath = Path.Combine(folderName, fileName);
 
            using (var stream = new FileStream(this.fullPath, FileMode.Create))
            {
                uploadedfile.CopyTo(stream);
            }


            using (var reader = new StreamReader(this.fullPath))
            using (var csv = new CsvReader(reader))
            {
            var FarmersTable = new List<Farmer>();
           


            csv.Read();
            csv.ReadHeader();

            
            while (csv.Read())
            {
            			
                        

    var farmer = new Farmer
            {

        Id = csv.GetField<int>("Id"),
       FirstName =csv.GetField("FirstName"),
       SurName = csv.GetField("Surname"),
       OtherName = csv.GetField("OtherName"),

        Address = csv.GetField("PlotAddress") +csv.GetField("StreetAddress")+csv.GetField("CityAddress"),

         FaceImageUrl = csv.GetField("FaceImage"),
       FarmImageUrl =csv.GetField("FarmImage"),
        FarmFarmerImage =csv.GetField("FarmImageFarmerStanding"),


        maritalStatusId =csv.GetField<int>("MaritalStatus"),

       genderId =csv.GetField<int>("Gender"),


        CommodityId =csv.GetField<int>("Commodity"),

      NoOfDependants =csv.GetField<int>("NumberofDependents"),

        nationalityId= csv.GetField<int>("Nationality"),

        OtherNationality = csv.GetField("OtherNationality"),


        stateoforiginId= csv.GetField<int>("StateofOrigin"),



       LgaoforiginId = csv.GetField<int>("LGAofOrigin"),



        stateofresidenceId = csv.GetField<int>("StateAddress"),

        LgaofresidenceId= csv.GetField<int>("LGAAddress"),

       // DateOfBirth = DateTime.Now,
       DateOfBirth=csv.GetField<DateTime>("DateOfBirth"),
       // DateTime.ParseExact(csv.GetField("DateOfBirth"), "ddd MMM dd HH:mm:ss 'UTC'zzz yyyy", null, DateTimeStyles.None),


        

        PhoneNumber = csv.GetField("FarmerPhonenumber"),
        CurrentIncome= csv.GetField<int>("CurrentIncome"),

        AlternativeOccupation =csv.GetField("alternativeoccupatiom"),

        PreviousInterventions =csv.GetField("alternativeoccupatiom"),

        //Guarantor

 

       GuarantorName=csv.GetField("NameOfGuarantor"),
       GuarantorAddress = csv.GetField("AddressofGuarantor"),

      GuarantorPhone= csv.GetField("PhoneNoofGuarantor"),
      GuarantorEmail = csv.GetField("Email"),

// Next of Kin


    NextofKinName =csv.GetField("NameOfNextOfKin"),
    NextofKinAddress =csv.GetField("Addressofnextofkin"),
    NextofKinPhone =csv.GetField("PhoneNoofNextOfKin"),

    //Identity

    

        

     

        

       IDtypeId= csv.GetField<int>("IDType"),
        
       
       BVN =csv.GetField("BVN Number"),
        IDNumber=csv.GetField("IDNo"),
        IDUrl = csv.GetField("IDImage"),
        IssueDate =  csv.GetField<DateTime>("IssueDate"),

        ExpiryDate =  csv.GetField<DateTime>("ExpiryDate"),

// Farmland Info

         FarmOwnershiptypeId =csv.GetField<int>("Landownership"),

        farmerVarificationStatusId = csv.GetField<int>("Verification_Status"),

        FarmVaricationDate =csv.GetField<DateTime>("Verification_Date"),
        HectreSize =csv.GetField<int>("FarmSize"),

        Topography =csv.GetField("Topography"),

         SoilType =csv.GetField("SoilType"),

         //Commodity=csv.GetField<int>("Commodity"),

         FarmLocation=csv.GetField("Farm_Location"),

        GPSCenterPoint ="0",
        GPSlatitude= csv.GetField("GPSCenterPoint:Latitude"),
        GPSlongitude= csv.GetField("GPSCenterPoint:Longitude"),
        GPS_altitude= csv.GetField("GPSCenterPoint:Altitude"),
        GPSCenterPoint_precision= csv.GetField("GPSCenterPoint:Accuracy"),
        SizeOfLand =csv.GetField<int>("Size_Of_Land"),
        AgentPhoneNumber =csv.GetField("Agent_Phone_Number"),

        MappingStatus =csv.GetField<int>("MappingStatus"),

        DisbursmentStatus =csv.GetField<int>("DisbursmentStatus"),
       


        };

    
    FarmersTable.Add(farmer);
    

             }

    this.BulkInsert(FarmersTable).Wait();

             
        }
    } 
     return  Task.CompletedTask;


  } 
 catch(Exception e){
    return Task.FromException(e);


} finally{

          DeleteFile(this.fullPath);

}


        
        
    }
    




    public Task DeleteFile(string Path)
    {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }

            return  Task.CompletedTask;


    }



 

    }


}