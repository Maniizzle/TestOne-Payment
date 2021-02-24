using AutoMapper;
using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Mapper
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentDetail, PaymentRequestDto>().ReverseMap();

        }
    }
}