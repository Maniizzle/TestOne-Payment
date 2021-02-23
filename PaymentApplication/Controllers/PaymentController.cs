using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IMapper mapper;

        public PaymentController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public IActionResult ProcessPayment(PaymentRequestDto paymentDetail )
        {
            if (ModelState.IsValid)
            {
                
                var answer = mapper.Map<PaymentDetail>(paymentDetail);

                return Ok(answerOptionsService.CreateAnswerOption(answer));
            }
            return BadRequest("Invalid Paramter");
            return View();
        }
       
    }
}
