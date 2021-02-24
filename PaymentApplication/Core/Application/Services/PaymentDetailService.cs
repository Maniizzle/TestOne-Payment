using PaymentApplication.Core.Application.Services.Interfaces;
using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Core.Domain.Repository;
using PaymentApplication.Utility;
using PaymentApplication.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentApplication.Helper;
using PaymentApplication.Core.Application.Services.Gateways;
using PaymentApplication.Dtos;

namespace PaymentApplication.Core.Application.Services
{
    public class PaymentDetailService:IPaymentDetailService
    {
        private readonly IPaymentLogRepository paymentLogRepository;
        private readonly IPaymentDetailRepository detailRepository;
        private readonly IExpensivePaymentGateway expensivePaymentGateway;
        private readonly ICheapPaymentGateway cheapPaymentGateway;
        private readonly IPremiumPaymentGateway premiumPaymentGateway;

        public PaymentDetailService(IPaymentLogRepository paymentLogRepository, IPaymentDetailRepository detailRepository,IExpensivePaymentGateway expensivePaymentGateway,ICheapPaymentGateway cheapPaymentGateway,IPremiumPaymentGateway premiumPaymentGateway)
        {
            this.paymentLogRepository = paymentLogRepository;
            this.detailRepository = detailRepository;
            this.expensivePaymentGateway = expensivePaymentGateway;
            this.cheapPaymentGateway = cheapPaymentGateway;
            this.premiumPaymentGateway = premiumPaymentGateway;
        }
        public Response<PaymentResponseDto> ProcessPayment(PaymentDetail paymentDetail, int transactionType = 0)
        {
            try
            {
                if (!paymentDetail.ValidatePaymentDetail())
                    return new Response<PaymentResponseDto> { Code = ResponseCodes.INVALID_REQUEST, Description = "Invalid Request", Payload = new PaymentResponseDto {Message= "Invalid Request", Amount=paymentDetail.Amount } };
                bool paymentStatus;

                switch (paymentDetail.Amount)
                {

                    //later move range to appsettings to make it dynamic
                    case var n when n > 0 && n < 21:

                        paymentStatus = ProcessPaymentLessThan21(paymentDetail,transactionType);
                        break;

                    case var n when n > 20 && n < 501:

                        paymentStatus = ProcessPaymentGreaterThan21LessThan501(paymentDetail,transactionType);
                        break;

                    case var n when n > 500:

                        paymentStatus = ProcessPaymentGreaterThan500(paymentDetail,transactionType);
                        break;

                    default:
                        paymentStatus = false;
                        break;
                }

                if (paymentStatus == true)
                {
                    return new Response<PaymentResponseDto> { Code = ResponseCodes.OK, Description = "Successful", Payload = new PaymentResponseDto { Message = "Invalid Request", Amount = paymentDetail.Amount } };

                }
                else
                {
                    return new Response<PaymentResponseDto> { Code = ResponseCodes.ERROR, Description = "Server Error", Payload = new PaymentResponseDto { Message = "Invalid Request", Amount = paymentDetail.Amount } };

                }
            }
            catch (Exception)
            {
                return new Response<PaymentResponseDto> { Code = ResponseCodes.ERROR, Description = "Server Error", Payload = new PaymentResponseDto { Message = "Invalid Request", Amount = paymentDetail.Amount } };
            }
        }


        public bool ProcessPaymentLessThan21(PaymentDetail paymentDetail, int transactionType = 0)
        {
            var referenceNumber = Utility.Utility.GenerateReferenceNumber();
            paymentDetail.PaymentReference = referenceNumber;
            paymentDetail.PaymentLogs = new List<PaymentLog> { new PaymentLog { Amount = paymentDetail.Amount, PaymentDate = DateTime.Now, PaymentState = PaymentState.Pending, PaymentGateway = PaymentGateway.CheapGateway, PaymentReference = referenceNumber } };
            detailRepository.Add(paymentDetail);
            detailRepository.Save();

            var gatewayResult = cheapPaymentGateway.ProcessPayment(paymentDetail,transactionType);
            var result = paymentLogRepository.Filter(c => c.PaymentReference == referenceNumber).FirstOrDefault();
            if (gatewayResult.Code == ResponseCodes.OK)
            {
                result.PaymentState = PaymentState.Processed;
                paymentLogRepository.Update(result);
                paymentLogRepository.Save();
                return true;
            }
            else
            {
                result.PaymentState = PaymentState.Failed;
                paymentLogRepository.Update(result);
                paymentLogRepository.Save();
                return false;
            }

        }

        public bool ProcessPaymentGreaterThan21LessThan501(PaymentDetail paymentDetail, int transactionType = 0)
        {
            var referenceNumberr = Utility.Utility.GenerateReferenceNumber();
            paymentDetail.PaymentReference = referenceNumberr;
            paymentDetail.PaymentLogs = new List<PaymentLog> { new PaymentLog { Amount = paymentDetail.Amount, PaymentDate = DateTime.Now, PaymentState = PaymentState.Pending, PaymentGateway = PaymentGateway.ExpensiveGateway, PaymentReference = referenceNumberr } };
            detailRepository.Add(paymentDetail);
            if (!(detailRepository.Save() > 0))
                return false;

            var expGatewayResult = expensivePaymentGateway.ProcessPayment(paymentDetail,transactionType);
            var paymentLog = paymentLogRepository.Filter(c => c.PaymentReference == referenceNumberr).FirstOrDefault();

            if (expGatewayResult.Code == ResponseCodes.OK)
            {
                paymentLog.PaymentState = PaymentState.Processed;
                paymentLogRepository.Update(paymentLog);
                paymentLogRepository.Save();
                return true;
            }
            else
            {
                paymentLog.PaymentState = PaymentState.Failed;
                paymentLogRepository.Update(paymentLog);
                paymentLogRepository.Save();


                //retry with cheapGateWay
                var logg = paymentLogRepository.Add(new PaymentLog { PaymentDetailId = paymentLog.PaymentDetailId, PaymentReference = paymentLog.PaymentReference, Amount = paymentDetail.Amount, PaymentGateway = PaymentGateway.CheapGateway, PaymentState = PaymentState.Pending });
                var cheapGatewayResult = cheapPaymentGateway.ProcessPayment(paymentDetail);
                var paymentLogCheap = paymentLogRepository.Filter(c => c.PaymentReference == referenceNumberr && c.PaymentGateway == PaymentGateway.CheapGateway).FirstOrDefault();
                if (cheapGatewayResult.Code == ResponseCodes.OK)
                {
                    paymentLog.PaymentState = PaymentState.Processed;
                    paymentLogRepository.Update(paymentLog);
                    paymentLogRepository.Save();
                    return true;
                }
                else
                {
                    paymentLog.PaymentState = PaymentState.Failed;
                    paymentLogRepository.Update(paymentLog);
                    paymentLogRepository.Save();
                    return false;
                }

            }

        }

   
        public bool ProcessPaymentGreaterThan500(PaymentDetail paymentDetail, int transactionType = 0)
        {
            var referenceNumberrr = Utility.Utility.GenerateReferenceNumber();
            paymentDetail.PaymentReference = referenceNumberrr;
            paymentDetail.PaymentLogs = new List<PaymentLog> { new PaymentLog { Amount = paymentDetail.Amount, PaymentDate = DateTime.Now, PaymentState = PaymentState.Pending, PaymentGateway = PaymentGateway.ExpensiveGateway, PaymentReference = referenceNumberrr } };
            detailRepository.Add(paymentDetail);
            detailRepository.Save();

            var premiumPaymentResult = premiumPaymentGateway.ProcessPayment(paymentDetail,transactionType);
            var paymentLogg = paymentLogRepository.Filter(c => c.PaymentReference == referenceNumberrr).FirstOrDefault();
            if (premiumPaymentResult.Code == ResponseCodes.OK)
            {
                paymentLogg.PaymentState = PaymentState.Processed;
                paymentLogRepository.Update(paymentLogg);
                paymentLogRepository.Save();
                return true;
            }
            else
            {

                paymentLogg.PaymentState = PaymentState.Failed;
                paymentLogRepository.Update(paymentLogg);
                paymentLogRepository.Save();


                int count = 1;
                while (count < 4)
                {
                    var logg = paymentLogRepository.Add(new PaymentLog { PaymentDetailId = paymentLogg.PaymentDetailId, PaymentReference = referenceNumberrr, Amount = paymentDetail.Amount, PaymentGateway = PaymentGateway.PremiumGateway, PaymentState = PaymentState.Pending });
                    if (!(paymentLogRepository.Save() > 0))
                        return false;
                    var premiumPaymentResultt = premiumPaymentGateway.ProcessPayment(paymentDetail,transactionType);
                    var paymentLogPremium = paymentLogRepository.Filter(c => c.PaymentReference == referenceNumberrr && c.PaymentGateway == PaymentGateway.PremiumGateway && c.PaymentState == PaymentState.Pending).FirstOrDefault();

                    if (premiumPaymentResultt.Code == ResponseCodes.OK)
                    {
                        paymentLogPremium.PaymentState = PaymentState.Processed;
                        paymentLogRepository.Update(paymentLogPremium);
                        paymentLogRepository.Save();
                        return true;
                    }
                    else
                    {
                        paymentLogPremium.PaymentState = PaymentState.Failed;
                        paymentLogRepository.Update(paymentLogPremium);
                        paymentLogRepository.Save();
                        count++;
                    }
                }
                return false;
            }

        }

    }
}
