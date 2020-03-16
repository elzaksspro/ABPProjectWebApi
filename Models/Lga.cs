namespace ProjectApi.Models
{
    public class Lga :BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int stateId { get; set; }


        public virtual State state { get; set; }

        
    }
}