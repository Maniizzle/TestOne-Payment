using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Core.Domain.Repository;
using PaymentApplication.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Persistence.Repository
{
    public class PaymentLogRepository:EntityRepository<PaymentLog>,IPaymentLogRepository
    {
        public PaymentLogRepository(PaymentAppDbContext context) : base(context)
        {

        }
    }
}
