namespace ServeMe.Repository.Models
{
    public class VendorDbModel
    {
        public int VendorID { get; set; }

        public string VendorName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool ReceiveCommunication { get; set; }

        public string Address { get; set; }

        public bool Agreement { get; set; }

        public float TotalEarnings { get; set; }

    }
}
