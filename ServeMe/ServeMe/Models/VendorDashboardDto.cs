using System;

namespace ServeMe.Models
{
    public class VendorDashboardDto
    {
        public double TotalEarnings { get; set; }

        public Orders Orders { get; set; }

        public double AverageRating { get; set; }
    }

    public class Orders
    {
        public DateTime Date { get; set; }

        public double Total { get; set; }
    }
}
