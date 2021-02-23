﻿using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Core.Application.Services.Gateways
{
    public abstract class BasePaymentGateway
    {
        public virtual Response<PaymentDetail> ProcessPayment()
        {
            return new Response<PaymentDetail>();
        }
    }
}