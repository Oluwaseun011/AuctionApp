namespace PaymentService.Application.Models
{
    /// <summary>
    /// Represents a data transfer object for generating a recipient.
    /// </summary>
    public class GenerateRecipientDTO
    {
        /// <summary>
        /// Gets or sets a value indicating the status of the operation.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the operation.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data for the generated recipient.
        /// </summary>
        public GenerateRecipientData Data { get; set; }
    }
}
