using Microsoft.EntityFrameworkCore;
using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Core.Domain.Repository;
using PaymentApplication.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PaymentApplication.Persistence.Repository
{
    public class PaymentLogRepository:EntityRepository<PaymentLog>,IPaymentLogRepository
    {
        public PaymentLogRepository(PaymentAppDbContext context) : base(context)
        {

        }

        public PaymentLog GetPaymentDetail(int id)
        {
           return Entities.Where(c => c.Id == id).Include(c => c.PaymentDetail).FirstOrDefault();
        }
        public PaymentLog GetPaymentLogByFilter(Expression<Func<PaymentLog, bool>> predicate)
        {
            return Entities.Where(predicate).Include(c => c.PaymentDetail).FirstOrDefault();
        }
    }
}
