using System;

namespace Bank.Models
{
    public class Customers : IDisposable
    {
	    public Dispose()
	    {
            GC.SuppressFinalize(this);
	    }


        public int CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string CustomerSurname { get; set; }

        public string CustomerPasskey { get; set; }

        public decimal Balance { get; set; }

        public byte BalanceType { get; set; }

        public bool isActive { get; set; }



    }
}
