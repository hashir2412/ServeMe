﻿using System;
using System.Collections.Generic;

namespace ServeMe.Models
{
    public class CartDto
    {
        public int CartId { get; set; }

        public int OrderId { get; set; }

        public int StatusId { get; set; }

        public int ServiceCategoryId { get; set; }

        public double Rate { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public ServiceCategoryDto ServiceCategory { get; set; }

        public List<BidDto> Bids { get; set; } = new List<BidDto>();

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Phone { get; set; }

        public string Name { get; set; }
        //public int CartId { get; set; }
        //public string Name { get; set; }

        //public int Quantity { get; set; }

        //public double Rate { get; set; }

        //public DateTime Date { get; set; }

        //public int StatusId { get; set; }

        //public ServiceCategoryDto ServiceCategory { get; set; }

        //public int VendorId { get; set; }

        //public int ServiceCategoryId { get; set; }
    }
}
