namespace PaymentService.Application.Models
{
    /// <summary>
    /// Represents the data model for verifying a bank account.
    /// </summary>
    public class VerifyBankData
    {
        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the account name.
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the bank code.
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// Gets or sets the bank identifier.
        /// </summary>
        public int BankId { get; set; }

        /// <summary>
        /// Gets or sets the recipient code.
        /// </summary>
        public string RecipientCode { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the integration ID.
        /// </summary>
        public int Integration { get; set; }

        /// <summary>
        /// Gets or sets the domain associated with the verification.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the amount involved in the verification.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency used in the verification.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the source of the verification.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the reason for the verification.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets the recipient identifier.
        /// </summary>
        public int Recipient { get; set; }

        /// <summary>
        /// Gets or sets the status of the verification.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the transfer code.
        /// </summary>
        public string TransferCode { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public string CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last updated date.
        /// </summary>
        public string UpdatedAt { get; set; }
    }
}
