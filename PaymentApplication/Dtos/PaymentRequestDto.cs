using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Dtos
{
    public class PaymentRequestDto
    {
        [Required]
        public string CreditCardNumber { get; set; }
        [Required]
        public string CardHolder { get; set; }
      [Required]
        public DateTime ExpiryDate { get; set; }
        public string SecurityCode { get; set; }
        [Required]
        [Range(1, (double)Decimal.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal Amount { get; set; }

        public bool Validate()
        {
            if ((ExpiryDate > DateTime.Today || ExpiryDate.ToShortDateString() == DateTime.Today.ToShortDateString()))
                return false;

            return true;
                    }
    }
}
