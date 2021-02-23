using PaymentApplication.Core.Application.Services.Interfaces;
using PaymentApplication.Core.Domain.Models;
using PaymentApplication.Core.Domain.Repository;
using PaymentApplication.Domain.Repository;
using PaymentApplication.Utility;
using PaymentApplication.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentApplication.Helper;
using PaymentApplication.Core.Application.Services.Gateways;

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
        public Response<PaymentDetail> ProcessPayment(PaymentDetail paymentDetail)
        {
            if (!paymentDetail.ValidatePaymentDetail())
                return new Response<PaymentDetail> { Code = ResponseCodes.INVALID_REQUEST, Description = "Invalid Request", Payload = paymentDetail };
            // var result= Utility.Utility.ValidateCreditCardNumber(paymentDetail.CreditCardNumber);
            if()
           // detailRepository.Add()
                return paymentDetail;
        }

        public PaymentDetail UpdatePaymentDetail(PaymentDetail paymentDetail)
        {
            throw new NotImplementedException();
        }

        private bool Proceeed(decimal amount,PaymentDetail paymentDetail)
        {
            switch (amount)
            {

                case var n when n>0 && n<21:
                    
                    var referenceNumber = Utility.Utility.GenerateReferenceNumber();
                    paymentDetail.PaymentReference = referenceNumber;
                    paymentDetail.PaymentLogs = new List<PaymentLog> { new PaymentLog { Amount = paymentDetail.Amount, PaymentDate = DateTime.Now, PaymentState = PaymentState.Pending, PaymentGateway = PaymentGateway.CheapGateway, PaymentReference = referenceNumber } };
                        detailRepository.Add(paymentDetail);
                        if(!(detailRepository.Save()>0))
                        return false;

                    var gatewayResult = cheapPaymentGateway.ProcessPayment(paymentDetail);
                    if (gatewayResult.Code == ResponseCodes.OK)
                    {
                        var result = paymentLogRepository.Filter(c => c.PaymentReference == referenceNumber).FirstOrDefault();
                        result.PaymentState = PaymentState.Processed;
                       // var log = paymentLogRepository.Add(new PaymentLog { PaymentDetailId = result.PaymentDetailId, PaymentReference = result.PaymentReference, Amount = gatewayResult.Payload.Amount, PaymentGateway = PaymentGateway.CheapGateway, PaymentState = PaymentState.Processed });
                        return true;
                    }
                    else
                    {
                        var result = detailRepository.Filter(c => c.PaymentReference == referenceNumber, "PaymentLog").FirstOrDefault();
                        var log = paymentLogRepository.Add(new PaymentLog { PaymentDetailId = result.Id, PaymentReference = result.PaymentReference, Amount = expGatewayResult.Payload.Amount, PaymentGateway = PaymentGateway.ExpensiveGateway, PaymentState = PaymentState.Failed });
                        return false;
                    }

                case var n when n > 20 && n < 501:
                    var referenceNumberr = Utility.Utility.GenerateReferenceNumber();
                    paymentDetail.PaymentReference = referenceNumberr;
                    paymentDetail.PaymentLogs = new List<PaymentLog> { new PaymentLog { Amount = paymentDetail.Amount, PaymentDate = DateTime.Now, PaymentState = PaymentState.Pending, PaymentGateway = PaymentGateway.ExpensiveGateway, PaymentReference = referenceNumberr } };
                    detailRepository.Add(paymentDetail);
                    if (!(detailRepository.Save() > 0))
                        return false;

                    var expGatewayResult = expensivePaymentGateway.ProcessPayment(paymentDetail);
                    if (expGatewayResult.Code == ResponseCodes.OK)
                    {
                        var result = detailRepository.Filter(c => c.PaymentReference == referenceNumberr, "PaymentLog").FirstOrDefault();
                        var log = paymentLogRepository.Add(new PaymentLog { PaymentDetailId = result.Id, PaymentReference = result.PaymentReference, Amount = expGatewayResult.Payload.Amount, PaymentGateway = PaymentGateway.ExpensiveGateway, PaymentState = PaymentState.Processed });
                        return true;
                    }
                    else
                    {
                        var result = detailRepository.Filter(c => c.PaymentReference == referenceNumberr, "PaymentLog").FirstOrDefault();
                        var log = paymentLogRepository.Add(new PaymentLog { PaymentDetailId = result.Id, PaymentReference = result.PaymentReference, Amount = expGatewayResult.Payload.Amount, PaymentGateway = PaymentGateway.ExpensiveGateway, PaymentState = PaymentState.Failed });

                        //retry with cheapGateWay
                        var cheapGatewayResult = cheapPaymentGateway.ProcessPayment(paymentDetail);
                        if (cheapGatewayResult.Code == ResponseCodes.OK)
                        {
                           // var resulttt = detailRepository.Filter(c => c.PaymentReference == referenceNumberr, "PaymentLog").FirstOrDefault();
                            var logg = paymentLogRepository.Add(new PaymentLog { PaymentDetailId = result.Id, PaymentReference = result.PaymentReference, Amount = expGatewayResult.Payload.Amount, PaymentGateway = PaymentGateway.CheapGateway, PaymentState = PaymentState.Processed });
                            return true;
                        }
                        else
                        {
                            var logg = paymentLogRepository.Add(new PaymentLog { PaymentDetailId = result.Id, PaymentReference = result.PaymentReference, Amount = expGatewayResult.Payload.Amount, PaymentGateway = PaymentGateway.CheapGateway, PaymentState = PaymentState.Failed });
                            return false;
                        }

                    }



                case var n when n > 500 :
                    var referenceNumberr = Utility.Utility.GenerateReferenceNumber();
                    paymentDetail.PaymentReference = referenceNumberr;
                    paymentDetail.PaymentLogs = new List<PaymentLog> { new PaymentLog { Amount = paymentDetail.Amount, PaymentDate = DateTime.Now, PaymentState = PaymentState.Pending, PaymentGateway = PaymentGateway.ExpensiveGateway, PaymentReference = referenceNumberr } };
                    detailRepository.Add(paymentDetail);
                    if (!(detailRepository.Save() > 0))
                        return false;

                    var expGatewayResult = expensivePaymentGateway.ProcessPayment(paymentDetail);
                    if (expGatewayResult.Code == ResponseCodes.OK)
                    {
                        var result = detailRepository.Filter(c => c.PaymentReference == referenceNumberr, "PaymentLog").FirstOrDefault();
                        var log = paymentLogRepository.Add(new PaymentLog { PaymentDetailId = result.Id, PaymentReference = result.PaymentReference, Amount = expGatewayResult.Payload.Amount, PaymentGateway = PaymentGateway.ExpensiveGateway, PaymentState = PaymentState.Processed });
                        return true;
                    }
                    else
                    {
                        var result = detailRepository.Filter(c => c.PaymentReference == referenceNumberr, "PaymentLog").FirstOrDefault();
                        var log = paymentLogRepository.Add(new PaymentLog { PaymentDetailId = result.Id, PaymentReference = result.PaymentReference, Amount = expGatewayResult.Payload.Amount, PaymentGateway = PaymentGateway.ExpensiveGateway, PaymentState = PaymentState.Failed });

                        //retry with cheapGateWay
                        var cheapGatewayResult = cheapPaymentGateway.ProcessPayment(paymentDetail);
                        if (cheapGatewayResult.Code == ResponseCodes.OK)
                        {
                            // var resulttt = detailRepository.Filter(c => c.PaymentReference == referenceNumberr, "PaymentLog").FirstOrDefault();
                            var logg = paymentLogRepository.Add(new PaymentLog { PaymentDetailId = result.Id, PaymentReference = result.PaymentReference, Amount = expGatewayResult.Payload.Amount, PaymentGateway = PaymentGateway.CheapGateway, PaymentState = PaymentState.Processed });
                            return true;
                        }
                        else
                        {
                            var logg = paymentLogRepository.Add(new PaymentLog { PaymentDetailId = result.Id, PaymentReference = result.PaymentReference, Amount = expGatewayResult.Payload.Amount, PaymentGateway = PaymentGateway.CheapGateway, PaymentState = PaymentState.Failed });
                            return false;
                        }

                    }


                default:
                    break;
            }
        }
    }
}
