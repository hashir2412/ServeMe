namespace ServeMe.Repository.Models
{
    public class BidDbModel
    {
        public int BidId { get; set; }

        public int CartId { get; set; }

        public int VendorId { get; set; }

        public double Amount { get; set; }
    }
}
