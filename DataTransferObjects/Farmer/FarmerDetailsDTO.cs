using System;
using ProjectApi.Models;


namespace ProjectApi.DataTransferObjects
{
    public class FarmerDetailsDTO
    {
       public int Id { get; set; }


        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string OtherName { get; set; }

        public string Address { get; set; }

        public string FaceImageUrl { get; set; }
        public string FarmImageUrl { get; set; }
        public string FarmFarmerImage { get; set; }


        public int  maritalStatusId { get; set; }

        public virtual MaritalStatus maritalStatus { get; set; }
 
        public int  genderId { get; set; }

        public  virtual Gender gender { get; set; }

        public int NoOfDependants { get; set; }

        public int  nationalityId { get; set; }

        public virtual Nationality nationality { get; set; }

        public string OtherNationality { get; set; }


        public int  stateoforiginId { get; set; }

        public virtual State stateoforigin { get; set; }


        public int  LgaoforiginId { get; set; }

        public virtual Lga Lgaoforigin { get; set; }

        public int  stateofresidenceId { get; set; }

        public virtual State stateofresidence { get; set; }


        public int  LgaofresidenceId { get; set; }

        public virtual Lga Lgaofresidence { get; set; }


        public int  CommodityId { get; set; }

        public virtual Commodity Commodity { get; set; }




        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public int CurrentIncome { get; set; }

        public string AlternativeOccupation { get; set; }

        public string PreviousInterventions { get; set; }
        

         //Guarantort





        public string GuarantorName { get; set; }
        public string GuarantorAddress { get; set; }
        public string GuarantorPhone { get; set; }

        public string GuarantorEmail { get; set; }
        

        //Next of Kin


      
        public string NextofKinName { get; set; }
        public string NextofKinAddress { get; set; }
        public string NextofKinPhone { get; set; }

//Identities


        public int IDtypeId { get; set; }

        public virtual IDType IDtype { get; set; }
        
        public string BVN { get; set; }

        public string IDNumber { get; set; }
     
        public string IDUrl { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
       
       

//Land Info


    
        public int FarmOwnershiptypeId { get; set; }
        public virtual FarmOwnershipType farmOwnershiptype { get; set; }

        public int farmerVarificationStatusId { get; set; }
        public virtual FarmerVarificationStatus farmerVarificationStatus { get; set; }

        public DateTime FarmVaricationDate { get; set; }



        public int  HectreSize { get; set; }

        public string  Topography { get; set; }

        public string  SoilType { get; set; }


        public string  FarmLocation { get; set; }

        public string  GPSCenterPoint { get; set; }
        public string  GPSlatitude { get; set; }
        public string  GPSlongitude { get; set; }
        public string  GPS_altitude { get; set; }
        public string  GPSCenterPoint_precision { get; set; }

        public int  SizeOfLand { get; set; }
        public string  AgentPhoneNumber { get; set; }



       

    }
}