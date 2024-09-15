namespace PaymentService.Application.Models
{
    /// <summary>
    /// Represents the data for a generated recipient.
    /// </summary>
    public class GenerateRecipientData
    {
        /// <summary>
        /// Gets or sets a value indicating whether the recipient is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the recipient was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the currency of the recipient.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the domain associated with the recipient.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the recipient.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the integration ID associated with the recipient.
        /// </summary>
        public int Integration { get; set; }

        /// <summary>
        /// Gets or sets the name of the recipient.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier for the recipient.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the reason for creating the recipient.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets the recipient code.
        /// </summary>
        public string RecipientCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the recipient.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the bank details associated with the recipient.
        /// </summary>
        public VerifyBankData Details { get; set; }
    }
}
