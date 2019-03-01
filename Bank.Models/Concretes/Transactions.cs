using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Models.Concretes
{ 
    public class Transactions : IDisposable
    {
	    public void Dispose()
	    {
            GC.SuppressFinalize(this);
	    }

        public Transactions()
        {
            Customer = new Customers();
        }

        public int TransactionID { get; set; }

        [Required(ErrorMessage = "You must enter an transaction amount.")]
        public decimal TransactionAmount { get; set; }

        [Required(ErrorMessage = "You must enter an sender account number.")]
        public int TransactorAccountNumber { get; set; }

        [Required(ErrorMessage = "You must enter an sender account number.")]
        public int? RecieverAccountNumber { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }

        public bool isSuccess { get; set; }

        public Customers Customer { get; set; }

    }
}
