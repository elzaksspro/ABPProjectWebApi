namespace ProjectApi.Params
{
    public class Params 
    {
         private const int MaxPageSize = 1000;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 100;
        public int PageSize
        {
            get { return pageSize;}
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value;}
        }

        public int UserId { get; set; }
        
    }
}