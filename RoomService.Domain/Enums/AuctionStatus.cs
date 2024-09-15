namespace RoomService.Domain.Enums
{
    public enum AuctionStatus
    {
        Pending,
        Active,
        Cancelled,
        Completed,
        ReserveNotMet,
        PaymentPending,
        Paid,
        Failed,
        Disputed
    }
}
