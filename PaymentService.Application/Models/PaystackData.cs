namespace PaymentService.Application.Models
{
    /// <summary>
    /// Represents the data provided by Paystack.
    /// </summary>
    public class PaystackData
    {
        /// <summary>
        /// Gets or sets the authorization URL for Paystack payment.
        /// </summary>
        public string AuthorizationUrl { get; set; }

        /// <summary>
        /// Gets or sets the access code for Paystack payment.
        /// </summary>
        public string AccessCode { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier for Paystack payment.
        /// </summary>
        public string Reference { get; set; }
    }
}
