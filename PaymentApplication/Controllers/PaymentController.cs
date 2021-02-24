using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaymentApplication.Core.Application.Services.Interfaces;
using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Controllers
{
    [Route("api/payment/")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPaymentDetailService payment;

        public PaymentController(IMapper mapper, IPaymentDetailService payment)
        {
            this.mapper = mapper;
            this.payment = payment;
        }
        /// <summary>
        /// Processes payment using several gateways
        /// </summary>
        /// <param name="paymentDetail"></param>
        /// <returns>status code and Message </returns>
        [HttpPost("processpayment")]
        public IActionResult ProcessPayment(PaymentRequestDto paymentDetail )
        {
            if (ModelState.IsValid)
            {
                
                var answer = mapper.Map<PaymentDetail>(paymentDetail);

              var result=  payment.ProcessPayment(answer);
                if(result.Code==Helper.ResponseCodes.INVALID_REQUEST)
                    return BadRequest("Request is Invalid");

                if (result.Code == Helper.ResponseCodes.ERROR)
                    return StatusCode(500,new { message="Internal Server Error" });

                if (result.Code == Helper.ResponseCodes.OK)
                    return Ok(new { message = "Payment is Processed" });

            }
            return BadRequest("Request is Invalid");
        }
       
    }
}
