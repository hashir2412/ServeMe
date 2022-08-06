namespace ServeMe.Models
{
    public class BidDto
    {
        public int BidId { get; set; }

        public int CartId { get; set; }

        public int VendorId { get; set; }

        public double Amount { get; set; }
    }
}
