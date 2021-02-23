using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Utility
{
    public class Utility
    {
        public static string GenerateReferenceNumber()
        {
           return "PAYMENT-" + DateTime.Now.ToString();
        }

		public static int GenerateRandomNumber()
        {
			var fff = new Random();
			var number = fff.Next(1, 2);
			return number;
		}

        public static bool  ValidateCreditCardNumber(string creditCardNumber)
        {

			int sumAllOtherNumber = 0;
			int sumMultipliedNumber = 0; 
			var length = creditCardNumber.Length;
			for (var i = length - 1; i >= 0; i -= 2)
			{
				int vv = int.Parse(creditCardNumber[i].ToString());
				sumAllOtherNumber += vv;
				if ((i - 1) >= 0)
				{
					var multipliedNumber = int.Parse(creditCardNumber[i - 1].ToString()) * 2;
					if (multipliedNumber.ToString().Length == 2)
					{
						var str = multipliedNumber.ToString();
						sumMultipliedNumber += int.Parse(str[0].ToString());
						sumMultipliedNumber += int.Parse(str[1].ToString());
					}
					else
					{
						sumMultipliedNumber += multipliedNumber;
					}
				}

			}

			var sum = sumMultipliedNumber + sumAllOtherNumber;
			var result = sum.ToString().Length;
			if (sum.ToString()[result - 1] == '0')
				return true;

			return false;

		}
    }
}
