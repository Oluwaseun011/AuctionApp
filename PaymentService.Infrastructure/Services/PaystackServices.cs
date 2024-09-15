using Microsoft.Extensions.Configuration;
using PaymentService.Application.Contracts;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PaymentService.Infrastructure.Services
{
    public class PaystackServices : IPaymentGateway
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaystackServices> _logger;
        public PaystackServices(HttpClient httpClient, IConfiguration configuration, ILogger<PaystackServices> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaystackResponse> MakePayment(double costOfProduct, string productNumber, string customerEmail)
        {
            var apiKey = _configuration["Paystack:APIKey"];
            if (string.IsNullOrEmpty(apiKey)) throw new InvalidOperationException("API Key is missing from the configuration.");

            costOfProduct *= 100; // Convert to kobo
            productNumber = string.IsNullOrWhiteSpace(productNumber)
                            ? Guid.NewGuid().ToString().Replace('-', 'y')
                            : productNumber;

            var request = new
            {
                amount = costOfProduct,
                email = customerEmail,
                reference = productNumber,
                currency = "NGN",
            };

            var response = await SendPostRequestAsync<PaystackResponse>("https://api.paystack.co/transaction/initialize", request, apiKey);
            return response;
        }

        public async Task<VerifyBank> VerifyByAccountNumber(string acNumber, string bankCode, decimal amount)
        {
            var apiKey = _configuration["Paystack:APIKey"];
            if (string.IsNullOrEmpty(apiKey)) throw new InvalidOperationException("API Key is missing from the configuration.");

            var uri = $"https://api.paystack.co/bank/resolve?account_number={acNumber}&bank_code={bankCode}";
            var response = await SendGetRequestAsync<VerifyBank>(uri, apiKey);
            return response;
        }

        public async Task<GenerateRecipientDTO> GenerateRecipients(VerifyBank verifyBank)
        {
            var apiKey = _configuration["Paystack:APIKey"];
            if (string.IsNullOrEmpty(apiKey)) throw new InvalidOperationException("API Key is missing from the configuration.");

            var request = new
            {
                type = "nuban",
                name = verifyBank?.Data?.AccountNumber ?? "Unknown Name",
                account_number = verifyBank?.Data?.AccountNumber ?? "Unknown Account Number",
                bank_code = verifyBank?.Data?.BankId.ToString() ?? "Unknown Bank Code",
                currency = "NGN",
            };

            var response = await SendPostRequestAsync<GenerateRecipientDTO>("https://api.paystack.co/transferrecipient", request, apiKey);
            return response;
        }

        public async Task<MakeATransfer> SendMoney(string recip, decimal amount)
        {
            var apiKey = _configuration["Paystack:APIKey"];
            if (string.IsNullOrEmpty(apiKey)) throw new InvalidOperationException("API Key is missing from the configuration.");

            var request = new
            {
                recipient = recip,
                amount = amount * 100, // Convert to kobo
                currency = "NGN",
                source = "balance"
            };

            var response = await SendPostRequestAsync<MakeATransfer>("https://api.paystack.co/transfer", request, apiKey);
            return response;
        }

        private async Task<T> SendPostRequestAsync<T>(string url, object request, string apiKey)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                var response = await _httpClient.PostAsync(url, content);

                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while making a POST request.");
                throw;
            }
        }

        private async Task<T> SendGetRequestAsync<T>(string url, string apiKey)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                var response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while making a GET request.");
                throw;
            }
        }
    }
}
