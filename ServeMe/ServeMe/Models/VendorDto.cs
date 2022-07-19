namespace ServeMe.Models
{
    public class VendorDto : BaseUserVendorDto
    {
        public int VendorId { get; set; }
        public bool Agreement { get; set; }

        public string Address { get; set; }

        public double TotalEarnings { get; set; }
    }
}
