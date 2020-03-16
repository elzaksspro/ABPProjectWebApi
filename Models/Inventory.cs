namespace ProjectApi.Models
{
    public class Inventory: BaseEntity
    {
    
    public int Id { get; set; }

    public int  EOPId { get; set; }

    public virtual EOP EOP { get; set; }
        
    public int  stateId { get; set; }

    public virtual State state { get; set; }
    
    public string Company { get; set; }
    public int  DeliveredQuantity { get; set; }
    public int  TargetQuantity { get; set; }


    
    }
}