using PaymentApplication.Core.Domain.Models;
using System;
using Xunit;

namespace PaymentApplication.Test
{
    public class PaymentDetailTest
    {

        [Fact]
        public void ValidateValidCreditCardNumber()
        {
            var testclass = new PaymentDetail();
            //To pass this test, KIndly enter a Valid CCN BELOW and set expected to true
            testclass.CreditCardNumber = "";
             var actual=  testclass.ValidateCreditCardNumber();
            var expected = false;
            Assert.Equal(expected, actual);


        }

        [Fact]
        public void ValidateInValidCreditCardNumber()
        {

            var testclass = new PaymentDetail();
            testclass.CreditCardNumber = "5199110721507909";
            var actual = testclass.ValidateCreditCardNumber();
            var expected = false;
            Assert.Equal(expected, actual);

        }
        [Fact]
        public void ValidateExpiryDateOnCreditCard()
        {

            var testclass = new PaymentDetail();
            testclass.ExpirationDate = new DateTime(2021,4,24);
            var actual = testclass.ValidateExpirationDate();
            var expected = true;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void ValidatePaymentDetail()
        {
            //change creditcard to a valid one and set expected to true
            var testclass = new PaymentDetail();
            testclass.CreditCardNumber = "5199110721565746";
            testclass.SecurityCode = "234";
            testclass.ExpirationDate = new DateTime(2021, 4, 24);
            testclass.Amount = 50;
            var actual = testclass.ValidatePaymentDetail();
            var expected = false;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void ValidateSecurityCode()
        {

            var testclass = new PaymentDetail();
           // testclass.SecurityCode = "234";
            var actual = testclass.ValidateSecurityCode();
            var expected = true;
            Assert.Equal(expected, actual);

        }

    }
}
