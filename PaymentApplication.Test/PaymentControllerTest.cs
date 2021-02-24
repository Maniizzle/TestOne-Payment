using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PaymentApplication.Controllers;
using PaymentApplication.Core.Application.Services;
using PaymentApplication.Core.Application.Services.Interfaces;
using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Dtos;
using PaymentApplication.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PaymentApplication.Test
{
   public class PaymentControllerTest
    {
        [Fact]
        public void ValidateRequestModelDtoUsingModelState()
        {
            var paymentRequest = new PaymentRequestDto();
            var paymentModel = new PaymentDetail();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(c=>c.Map<PaymentDetail>(paymentRequest)).Returns(paymentModel);
            var mockService = new Mock<IPaymentDetailService>();
            mockService.Setup(repo => repo.ProcessPayment(paymentModel,2))
                .Returns(new Response<Dtos.PaymentResponseDto> {Code= ResponseCodes.OK });
            var controller = new PaymentController(mapperMock.Object, mockService.Object);


           // controller.ModelState.AddModelError("Invalid Parameter", "Required");
            var req= new PaymentRequestDto { Amount = 20, CardHolder = "Olamide", ExpirationDate = DateTime.Now };

            // Act
            var result =  controller.ProcessPayment(req);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Request is Invalid",badRequestResult.Value);

        }

       
    }
}
