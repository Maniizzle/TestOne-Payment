using PaymentApplication.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PaymentApplication.Core.Domain.Repository
{
    public interface IPaymentDetailRepository:IEntityRepository<PaymentDetail>
    {
        PaymentDetail GetPaymentDetailById(int id);
        public PaymentDetail GetPaymentDetailByFilter(Expression<Func<PaymentDetail, bool>> predicate);


    }
}
