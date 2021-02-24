using Moq;
using PaymentApplication.Core.Application.Services.Gateways;
using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PaymentApplication.Test
{
  public  class PaymentGatewayTest
    {
        [Fact]
        public void ValidateSuccessfulCheapPaymentGatewayPayment()
        {

            var paymentDetail = new PaymentDetail();
            var cheapPaymentGateway = new CheapPaymentGateway();
            var expected = ResponseCodes.OK;
            //odd numbers fail, even(2,4,6) numbers succeed. I used 2 below
            var result= cheapPaymentGateway.ProcessPayment(paymentDetail, 2);
            var actual = result.Code;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void ValidateFailedCheapPaymentGatewayPayment()
        {
            var paymentDetail = new PaymentDetail();
            var cheapPaymentGateway = new CheapPaymentGateway();
            var expected = ResponseCodes.ERROR;
            var result = cheapPaymentGateway.ProcessPayment(paymentDetail, 1);
            var actual = result.Code;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void ValidateSuccessfulExpensiveGatewayPayment()
        {
            var paymentDetail = new PaymentDetail();
            var expensivePaymentGateway = new ExpensivePaymentGateway();
            var expected = ResponseCodes.OK;
            //odd numbers fail, even(2,4,6) numbers succeed. I used 2 below
            var result = expensivePaymentGateway.ProcessPayment(paymentDetail, 2);
            var actual = result.Code;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void ValidateFailedExpensiveGatewayPayment()
        {
            var paymentDetail = new PaymentDetail();
            var expensivePaymentGateway = new ExpensivePaymentGateway();
            var expected = ResponseCodes.ERROR;
            var result = expensivePaymentGateway.ProcessPayment(paymentDetail, 1);
            var actual = result.Code;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void ValidateSuccessfulPremiumPaymentGatewayPayment()
        {
            var paymentDetail = new PaymentDetail();
            var premiumPaymentGateway = new PremiumPaymentGateway();
            var expected = ResponseCodes.OK;
            //odd numbers fail, even(2,4,6) numbers succeed. I used 2 below
            var result = premiumPaymentGateway.ProcessPayment(paymentDetail, 2);
            var actual = result.Code;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void ValidateFailedPremiumPaymentGatewayPayment()
        {
            var paymentDetail = new PaymentDetail();
            var premiumPaymentGateway = new PremiumPaymentGateway();
            var expected = ResponseCodes.ERROR;
            var result = premiumPaymentGateway.ProcessPayment(paymentDetail, 1);
            var actual = result.Code;
            Assert.Equal(expected, actual);

        }


    }
}
