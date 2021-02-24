using Microsoft.AspNetCore.Http;
using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Core.Application.Services.Gateways
{
    public interface IPremiumPaymentGateway
    {
        Response<PaymentDetail> ProcessPayment(PaymentDetail paymentDetail, int number = 0);
     

    }
}
