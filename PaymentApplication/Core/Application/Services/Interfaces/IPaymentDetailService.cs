using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Dtos;
using PaymentApplication.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Core.Application.Services.Interfaces
{
    public interface IPaymentDetailService
    {

        Response<PaymentResponseDto> ProcessPayment(PaymentDetail paymentDetail,int transactionType=0);

      //  PaymentDetail UpdatePaymentDetail(PaymentDetail paymentDetail);

    }
}
