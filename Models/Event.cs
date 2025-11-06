namespace EventEase.Models
{
    public class Event
    {
        // Unique identifier for the event
        public int Id { get; set; }
        
        // Event name/title
        public string Name { get; set; } = string.Empty;
        
        // Detailed description of the event
        public string Description { get; set; } = string.Empty;
        
        // When the event occurs
        public DateTime Date { get; set; }
        
        // Physical location or venue
        public string Location { get; set; } = string.Empty;
        
        // Event category (Technology, Business, Arts, etc.)
        public string Category { get; set; } = string.Empty;
        
        // Maximum number of people who can attend
        public int MaxAttendees { get; set; }
        
        // Current number of registered attendees
        public int CurrentAttendees { get; set; }
        
        // Ticket price (0 for free events)
        public decimal Price { get; set; }
        
        // URL to event image
        public string ImageUrl { get; set; } = "/images/default-event.jpg";
        
        // Whether the event is active and accepting registrations
        public bool IsActive { get; set; } = true;
    }
}