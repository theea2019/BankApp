using System;

namespace Bank.Models.Concretes
{
    public class Customers : IDisposable
    {
	    public void Dispose()
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
