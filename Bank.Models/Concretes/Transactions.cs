using System;

namespace Bank.Models.Concretes
{ 
    public class Transactions : IDisposable
    {
	    public void Dispose()
	    {
            GC.SuppressFinalize(this);
	    }

        public int TransactionID { get; set; }
 
        public decimal TransactionAmount { get; set; }

        public int TransactorAccountNumber { get; set; }

        public int? RecieverAccountNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        public bool isSuccess { get; set; }

    }
}
