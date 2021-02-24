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
        [MaxLength(19)]
        public string CreditCardNumber { get; set; }
        [Required]
        public string CardHolder { get; set; }
      [Required]
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        [Required]
        [Range(1, (double)Decimal.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal Amount { get; set; }

       
    }
}
