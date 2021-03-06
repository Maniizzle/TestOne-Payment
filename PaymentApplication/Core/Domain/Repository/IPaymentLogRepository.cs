﻿using PaymentApplication.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PaymentApplication.Core.Domain.Repository
{
   public interface IPaymentLogRepository : IEntityRepository<PaymentLog>
    {
        PaymentLog GetPaymentDetail(int id);
        public PaymentLog GetPaymentLogByFilter(Expression<Func<PaymentLog, bool>> predicate);


    }
}
