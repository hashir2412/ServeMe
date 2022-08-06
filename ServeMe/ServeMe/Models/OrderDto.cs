using System;
using System.Collections.Generic;

namespace ServeMe.Models
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Phone { get; set; }

        public DateTime Date { get; set; }

        public double Total { get; set; }

        public List<CartDto> Items { get; set; }
    }
}
