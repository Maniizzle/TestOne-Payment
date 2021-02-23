using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Core.Domain.Models
{
   
        public class PaymentLog
        {
            public int Id { get; set; }
            public decimal Amount { get; set; }
            public string PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
             public PaymentGateway PaymentGateway { get; set; }
        public PaymentDetail PaymentDetail { get; set; }
        public int PaymentDetailId { get; set; }
        public PaymentState PaymentState { get; set; }
    }

    
}
