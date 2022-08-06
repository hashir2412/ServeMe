namespace ServeMe.Models
{
    public class OrderRequestModel
    {
        public ServiceCategoryDto[] Items { get; set; }

        public double Total { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Phone { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public string Email { get; set; }
    }
}
