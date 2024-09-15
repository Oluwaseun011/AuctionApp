namespace PaymentService.Application.Models
{
    /// <summary>
    /// Represents the response from Paystack after a payment attempt.
    /// </summary>
    public class PaystackResponse
    {
        /// <summary>
        /// Gets or sets a value indicating the status of the Paystack response.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the Paystack response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the Paystack response.
        /// </summary>
        public PaystackData Data { get; set; }
    }
}
