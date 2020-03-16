using System;
using System.Collections;
using System.Collections.Generic;



namespace ProjectApi.Models
{
    public class EOP : BaseEntity
    {
        public int Id { get; set; }

        
        public string ComponentName { get; set; }
        public string Brand { get; set; }
        public int  Qty { get; set; }
        public int  RatePerUnit { get; set; }


        public int  EOPUnitId { get; set; }

        public virtual EOPUnit EOPUnit { get; set; }


        public int  EOPTypeId { get; set; }

        public virtual EOPType EOPType { get; set; }


        public int  CommodityId { get; set; }

        public virtual Commodity Commodity { get; set; }
    }
}