namespace LMCEvents.DTOs
{
    public class BookingResponseDTO
    {
        public string PersonName { get; set; }
        public long Quantity { get; set; }
        public decimal BookingPrice { get; set; }
        public string EventTitle { get; set; }
        public string? EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocal { get; set; }
        public string? EventAddress { get; set; }        

    }
}
