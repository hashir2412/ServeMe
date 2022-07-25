namespace ServeMe.Repository.Models
{
    public class ServiceDbModel
    {
        public int ServiceID { get; set; }

        public int VendorID { get; set; }

        public int RateType { get; set; }

        public double Rate { get; set; }

        public string Name { get; set; }

        public int ServiceCategoryID { get; set; }
    }
}
