namespace PaymentService.Application.Models
{
    /// <summary>
    /// Represents a response model for making a transfer.
    /// </summary>
    public class MakeATransfer
    {
        /// <summary>
        /// Gets or sets a value indicating the status of the transfer.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the transfer.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data for the transfer.
        /// </summary>
        public MakeATransferData Data { get; set; }
    }
}
