using System;

namespace ServeMe.Repository.Models
{
    public class OrderDbModel
    {
        public int OrderID { get; set; }

        public int UserID { get; set; }

        public int StatusID { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public double Total { get; set; }

        public StatusDbModel Status { get; set; }

        public UserDbModel User { get; set; }

    }
}
