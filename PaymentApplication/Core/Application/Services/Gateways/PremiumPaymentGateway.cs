using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Core.Application.Services.Gateways
{
    public class PremiumPaymentGateway:IPremiumPaymentGateway
    {
       
        public Response<PaymentDetail> ProcessPayment(PaymentDetail paymentDetail)
        {
            var number = Utility.Utility.GenerateRandomNumber();
            if (number % 2 == 0)
                return new Response<PaymentDetail> { Code = ResponseCodes.OK, Description = "Sucess", Payload = paymentDetail };

            return new Response<PaymentDetail> { Code = ResponseCodes.ERROR, Description = "Error", Payload = paymentDetail };
        }
    }
}
