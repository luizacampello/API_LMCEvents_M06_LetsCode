using System.Text.Json;
using System.Text.Json.Serialization;

namespace LMCEvents.DTOs
{
    public class BookingResponseDTO
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? EventTitle { get; private set; }

        public string PersonName { get; set; }        
        public long Quantity { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? BookingPrice { get; private set; }        
        public long IdEvent { private get; set; }

        
        
        public long GetIdEvent()
        {
            return IdEvent;
        }

        public void SetTitle(string title)
        {
            EventTitle = title;
        }

        public void SetBookingPrice(decimal unitPrice)
        {
            BookingPrice = unitPrice * Quantity;
        }
    }
}
