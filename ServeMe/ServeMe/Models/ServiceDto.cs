namespace ServeMe.Models
{
    public class ServiceDto
    {
        public int ServiceID { get; set; }
        public int RateType { get; set; }

        public double Rate { get; set; }

        public string Name { get; set; }

        public int VendorId { get; set; }

        public int ServiceCategoryId { get; set; }
    }
}
