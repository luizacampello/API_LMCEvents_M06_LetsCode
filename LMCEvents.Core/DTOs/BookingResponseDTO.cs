using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LMCEvents.DTOs
{
    public class BookingResponseDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [MinLength(2)]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória")]
        [Range(1, long.MaxValue)]
        public long Quantity { get; set; }

        [Required(ErrorMessage = " O identificador do evento é obrigatório")]
        [Range(1, long.MaxValue)]
        public long IdEvent { private get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? EventInfo { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal BookingPrice { get; private set; }

        public long GetIdEvent()
        {
            return IdEvent;
        }

        public void SetEventInfo(string info)
        {
            EventInfo = info;
        }

        public void SetBookingPrice(decimal unitPrice)
        {
            BookingPrice = unitPrice * Quantity;
        }
    }
}
