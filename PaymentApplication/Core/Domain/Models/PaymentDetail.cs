using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Core.Domain.Models
{
    public class PaymentDetail
    {
        public int Id { get; set; }
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
         public string PaymentReference { get; set; }
        public string SecurityCode { get; set; }
		public Decimal Amount { get; set; }
		public DateTime DateCreated { get; set; } = DateTime.Now;
        public ICollection<PaymentLog> PaymentLogs { get; set; }



		private  bool ValidateCreditCardNumber()
		{
			bool status = int.TryParse(CreditCardNumber.Trim(), out int checkResult);
			if (!status)
				return false;

			var creditCardNumber = CreditCardNumber;
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

		private bool ValidateExpirationDate()
        {
			return DateTime.Today <= ExpirationDate;
        }
		private bool ValidateAmount()
		{
			return Amount > 0;
		}
		private bool ValidateSecurityCode()
		{
            if (SecurityCode != null)
            {
				var securityCode = SecurityCode.Trim();
				var status = int.TryParse(securityCode, out int result);
                if (status)
                {
					if (securityCode.Length == 3)
						return true;
                }
				return false;
            }
			return false;
		}
		public bool ValidatePaymentDetail()
        {
			if (!ValidateExpirationDate())
				return false;

			if (!ValidateCreditCardNumber())
				return false;

			if (!ValidateSecurityCode())
				return false;

			if (!ValidateAmount())
				return false;

			return true;
		}
	}
}
