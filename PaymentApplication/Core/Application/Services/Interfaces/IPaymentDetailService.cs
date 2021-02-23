using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Core.Application.Services.Interfaces
{
    public interface IPaymentDetailService
    {

        Response<PaymentDetail> ProcessPayment(PaymentDetail paymentDetail);

        PaymentDetail UpdatePaymentDetail(PaymentDetail paymentDetail);

    }
}
