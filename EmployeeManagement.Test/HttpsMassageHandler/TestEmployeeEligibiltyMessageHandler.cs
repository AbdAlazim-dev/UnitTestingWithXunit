

using EmployeeManagement.Business;
using System.Text;
using System.Text.Json;

namespace EmployeeManagement.Test.HttpsMassageHandler
{
    public class TestEmployeeEligibiltyMessageHandler : HttpMessageHandler
    {
        private readonly bool _isEligible;

        public TestEmployeeEligibiltyMessageHandler(bool isEligible)
        {
            _isEligible = isEligible;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var promotionEligibilty = new PromotionEligibility()
            {
                EligibleForPromotion = _isEligible
            };

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(promotionEligibilty,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                }),
                Encoding.ASCII, "application/json")
            };
            return Task.FromResult(response);
        }
    }
}
