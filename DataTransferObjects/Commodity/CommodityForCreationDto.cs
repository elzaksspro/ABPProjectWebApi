using System.ComponentModel.DataAnnotations;

public class CommodityForCreationDto
{   
    public int Id { get; set; }
 
    [Required(ErrorMessage = "Name is required")]
    [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
    public string Name { get; set; }

}