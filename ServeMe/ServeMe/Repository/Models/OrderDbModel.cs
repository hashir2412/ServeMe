using System;

namespace ServeMe.Repository.Models
{
    public class OrderDbModel
    {
        public int OrderID { get; set; }

        public string Name { get; set; }

        public int UserID { get; set; }

        public int StatusID { get; set; }

        public DateTime Date { get; set; }

        public double Total { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Phone { get; set; }

        public StatusDbModel Status { get; set; }

        public UserDbModel User { get; set; }

    }
}
