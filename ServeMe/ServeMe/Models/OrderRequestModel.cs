namespace ServeMe.Models
{
    public class OrderRequestModel
    {
        public ItemRequestModel[] Items { get; set; }

        public string PaymentType { get; set; }

        public double Total { get; set; }

        public string Address { get; set; }

        public int UserID { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}
