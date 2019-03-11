using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bank.Models.Concretes
{
    
    public class Customers : IDisposable
    {
	    public void Dispose()
	    {
            GC.SuppressFinalize(this);
	    }

        public Customers()
        {
            Transactions = new List<Transactions>();
        }

        public int CustomerID { get; set; }

        [Required(ErrorMessage = "You must enter an name.")]
        [StringLength(50, MinimumLength = 3)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "You must enter an surname.")]
        [StringLength(50, MinimumLength = 3)]
        public string CustomerSurname { get; set; }

        [Required(ErrorMessage = "You must enter an passkey.")]
        [StringLength(50, MinimumLength = 3)]
        public string CustomerPasskey { get; set; }

        [Required(ErrorMessage = "You must enter an customer balance.")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "You must enter an customer balance type.")]
        public byte BalanceType { get; set; }

        public bool isActive { get; set; }

       
        public List<Transactions> Transactions { get; set; }
        
    }
}
