﻿namespace LMCEvents.DTOs
{
    public class EventResponseDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateHourEvent { get; set; }
        public string Local { get; set; }
        public string? Address { get; set; }
        public decimal Price { get; set; }
    }
}
