using System;

namespace ServeMe.Repository.Models
{
    public class PaymentDbModel
    {
        public int PaymentID { get; set; }

        public string PaymentType { get; set; }

        public int OrderID { get; set; }

        public int UserID { get; set; }

        public double TotalAmount { get; set; }

        public double CommissionDeducted { get; set; }

        public DateTime Date { get; set; }

       

    }
}
