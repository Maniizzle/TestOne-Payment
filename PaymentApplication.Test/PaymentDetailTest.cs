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
            //To pass this tes, KIndly enter a Valid CCN BELOW
            testclass.CreditCardNumber = "";
             var actual=  testclass.ValidateCreditCardNumber();
            var expected = true;
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

            var testclass = new PaymentDetail();
            testclass.ExpirationDate = new DateTime(2021, 4, 24);
            var actual = testclass.ValidateExpirationDate();
            var expected = true;
            Assert.Equal(expected, actual);

        }

    }
}
