using Microsoft.EntityFrameworkCore;
using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Core.Domain.Repository;
using PaymentApplication.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Persistence.Repository
{
    public class PaymentDetailRepository:EntityRepository<PaymentDetail>,IPaymentDetailRepository
    {
        public PaymentDetailRepository(PaymentAppDbContext context):base(context)
        {

        }

        public PaymentDetail GetPaymentDetailById(int id)
        {
           return Entities.Where(c => c.Id == id).Include(c => c.PaymentLogs).FirstOrDefault();
        }
    }
}
