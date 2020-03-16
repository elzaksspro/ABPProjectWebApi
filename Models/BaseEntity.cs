using System;

namespace ProjectApi.Models
{
    public  class BaseEntity
    {
        //public int Index { get; set; }

        //public int Id { get; set; }



       public DateTime CreatedDate { get; set; }


        public DateTime ModifiedDate { get;set;}
       

        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }
        
    }
}