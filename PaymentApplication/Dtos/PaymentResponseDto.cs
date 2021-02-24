using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Dtos
{
    public class PaymentResponseDto
    {
        public Decimal Amount { get; set; }
        public string Message { get; set; }
    }
}
