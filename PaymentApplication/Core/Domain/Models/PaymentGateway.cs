using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Core.Domain.Models
{
    public enum PaymentGateway
    {
        CheapGateway,
        ExpensiveGateway,
        PremiumGateway
    }

    public enum PaymentState
    {
        Pending,
        Processed,
        Failed
    }
    
}
