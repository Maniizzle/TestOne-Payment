using PaymentApplication.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Core.Domain.Repository
{
   public interface IPaymentLogRepository : IEntityRepository<PaymentLog>
    {

    }
}
