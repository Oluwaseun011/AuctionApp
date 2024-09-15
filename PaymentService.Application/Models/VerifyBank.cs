namespace PaymentService.Application.Models
{
    /// <summary>
    /// Represents the response model for verifying a bank account.
    /// </summary>
    public class VerifyBank
    {
        /// <summary>
        /// Gets or sets a value indicating the status of the verification.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the verification.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the bank verification.
        /// </summary>
        public VerifyBankData Data { get; set; }
    }
}
