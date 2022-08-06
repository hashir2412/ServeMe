namespace ServeMe.Repository.Models
{
    public class ServiceDbModel
    {
        public int ServiceID { get; set; }

        public int VendorID { get; set; }

        public int ServiceCategoryID { get; set; }

        public int Status { get; set; }

        public VendorDbModel Vendor { get; set; }

        public ServiceCategoryDbModel ServiceCategory { get; set; }
    }
}
