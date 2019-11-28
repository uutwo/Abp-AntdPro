using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.UI;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

namespace TuDou.Grace.MultiTenancy.Payments.Paypal
{
    public class PayPalGatewayManager : GraceServiceBase, ITransientDependency
    {
        private readonly PayPalHttpClient _client;
        
        public PayPalGatewayManager(PayPalPaymentGatewayConfiguration configuration)
        {
            var environment = GetEnvironment(configuration);
            _client = new PayPalHttpClient(environment);
        }

        private PayPalEnvironment GetEnvironment(PayPalPaymentGatewayConfiguration configuration)
        {
            switch (configuration.Environment)
            {
                case "sandbox":
                    {
                        return new SandboxEnvironment(configuration.ClientId, configuration.ClientSecret);
                    }
                case "live":
                    {
                        return new LiveEnvironment(configuration.ClientId, configuration.ClientSecret);
                    }
                default:
                    {
                        throw new ApplicationException("Unknown PayPal environment");
                    }
            }
        }

        public async Task<string> ExecutePaymentAsync(PayPalExecutePaymentRequestInput input)
        {
            var request = new OrdersCaptureRequest(input.PaymentId);
            request.RequestBody(new OrderActionRequest());

            var response = await _client.Execute(request);
            var payment = response.Result<Order>();
            if (payment.Status != "COMPLETED")
            {
                throw new UserFriendlyException(L("PaymentFailed"));
            }

            return payment.Id;
        }
    }
}