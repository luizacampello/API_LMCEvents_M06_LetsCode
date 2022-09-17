using System.ComponentModel.DataAnnotations;

namespace LMCEvents.DTOs
{
    public class EventResponseDTO
    {
        [Required(ErrorMessage = "Título do evento é obrigatório")]
        [MinLength(2)]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Data e horário do evento são obrigatórias. Formato")]
        public DateTime DateHourEvent { get; set; }

        [Required(ErrorMessage = "Local do evento é obrigatório")]
        [MinLength(2)]
        public string Local { get; set; }

        public string? Address { get; set; }

        [Required(ErrorMessage = "Preço do evento é obrigatório")]
        [Range(0, long.MaxValue)]
        public decimal Price { get; set; }
    }
}
