namespace PaymentService.Application.Models
{
    /// <summary>
    /// Represents the data model for a transfer.
    /// </summary>
    public class MakeATransferData
    {
        /// <summary>
        /// Gets or sets the reference identifier for the transfer.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the integration ID associated with the transfer.
        /// </summary>
        public int Integration { get; set; }

        /// <summary>
        /// Gets or sets the domain associated with the transfer.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the amount involved in the transfer.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency used in the transfer.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the source of the transfer.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the reason for the transfer.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets the recipient identifier for the transfer.
        /// </summary>
        public int Recipient { get; set; }

        /// <summary>
        /// Gets or sets the status of the transfer.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the transfer code.
        /// </summary>
        public string TransferCode { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the transfer.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the transfer.
        /// </summary>
        public string CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last updated date of the transfer.
        /// </summary>
        public string UpdatedAt { get; set; }
    }
}
