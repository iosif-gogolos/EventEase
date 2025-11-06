// PURPOSE: Service layer - handles all event-related business logic and data operations
// LOCATION: Services folder in EventEase project
using EventEase.Models;

namespace EventEase.Services
{
    // Interface defining the contract for event operations
    public interface IEventService
    {
        // Retrieves all events from the data source
        Task<List<Event>> GetEventsAsync();
        
        // Retrieves a single event by its ID
        Task<Event?> GetEventByIdAsync(int id);
        
        // Retrieves events filtered by category
        Task<List<Event>> GetEventsByCategoryAsync(string category);
        
        // Registers a user for an event
        Task<bool> RegisterForEventAsync(int eventId, string userEmail);
        
        // Gets all unique event categories
        Task<List<string>> GetCategoriesAsync();
    }

    // Implementation of IEventService with sample data
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;
        private List<Event>? _cachedEvents; // In-memory cache for events
        private DateTime? _cacheExpiry; // Cache expiration time
        private readonly TimeSpan _cacheLifetime = TimeSpan.FromMinutes(5); // Cache duration

        public EventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Retrieves events with caching to reduce redundant calls
        public async Task<List<Event>> GetEventsAsync()
        {
            // Return cached data if still valid
            if (_cachedEvents != null && _cacheExpiry > DateTime.Now)
            {
                return _cachedEvents;
            }

            try
            {
                // Simulate network delay for realistic async behavior
                await Task.Delay(100);
                
                _cachedEvents = GetSampleEvents();
                _cacheExpiry = DateTime.Now.Add(_cacheLifetime);
                
                return _cachedEvents;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching events: {ex.Message}");
                return new List<Event>();
            }
        }

        // Retrieves a specific event by ID
        public async Task<Event?> GetEventByIdAsync(int id)
        {
            try
            {
                var events = await GetEventsAsync();
                return events.FirstOrDefault(e => e.Id == id && e.IsActive);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching event {id}: {ex.Message}");
                return null;
            }
        }

        // Filters events by category
        public async Task<List<Event>> GetEventsByCategoryAsync(string category)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category))
                    return await GetEventsAsync();

                var events = await GetEventsAsync();
                return events.Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase) && e.IsActive).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching events for category {category}: {ex.Message}");
                return new List<Event>();
            }
        }

        // Registers a user for an event with validation
        public async Task<bool> RegisterForEventAsync(int eventId, string userEmail)
        {
            try
            {
                // Validate inputs
                if (eventId <= 0 || string.IsNullOrWhiteSpace(userEmail))
                    return false;

                // Simulate registration process
                await Task.Delay(200);
                
                var events = await GetEventsAsync();
                var eventItem = events.FirstOrDefault(e => e.Id == eventId);
                
                // Check if event is available for registration
                if (eventItem != null && 
                    eventItem.IsActive && 
                    eventItem.CurrentAttendees < eventItem.MaxAttendees &&
                    eventItem.Date > DateTime.Now)
                {
                    eventItem.CurrentAttendees++;
                    // Invalidate cache to reflect changes
                    _cacheExpiry = null;
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering for event {eventId}: {ex.Message}");
                return false;
            }
        }

        // Extracts all unique categories from events
        public async Task<List<string>> GetCategoriesAsync()
        {
            try
            {
                var events = await GetEventsAsync();
                return events
                    .Where(e => !string.IsNullOrWhiteSpace(e.Category))
                    .Select(e => e.Category)
                    .Distinct()
                    .OrderBy(c => c)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching categories: {ex.Message}");
                return new List<string>();
            }
        }

        // Sample data generator for demonstration purposes
        // In production, this would be replaced with API calls
        private List<Event> GetSampleEvents()
        {
            return new List<Event>
            {
                new Event
                {
                    Id = 1,
                    Name = "Tech Innovation Summit 2024",
                    Description = "Join industry leaders and innovators for a day of cutting-edge technology discussions, networking, and hands-on workshops. Explore the latest trends in AI, cloud computing, and digital transformation.",
                    Date = DateTime.Now.AddDays(15),
                    Location = "San Francisco Convention Center, CA",
                    Category = "Technology",
                    MaxAttendees = 500,
                    CurrentAttendees = 287,
                    Price = 299.99m,
                    ImageUrl = "/images/tech-summit.jpg",
                    IsActive = true
                },
                new Event
                {
                    Id = 2,
                    Name = "Business Leadership Workshop",
                    Description = "Develop your leadership skills with renowned business coaches and successful entrepreneurs. Learn practical strategies for team management, decision-making, and organizational growth.",
                    Date = DateTime.Now.AddDays(8),
                    Location = "New York Business Center, NY",
                    Category = "Business",
                    MaxAttendees = 150,
                    CurrentAttendees = 89,
                    Price = 175.00m,
                    ImageUrl = "/images/business-workshop.jpg",
                    IsActive = true
                },
                new Event
                {
                    Id = 3,
                    Name = "Contemporary Art Exhibition Opening",
                    Description = "Experience an exclusive preview of groundbreaking contemporary art from emerging and established artists. Enjoy wine, networking, and inspiring conversations about modern artistic expression.",
                    Date = DateTime.Now.AddDays(5),
                    Location = "Modern Art Gallery, Los Angeles, CA",
                    Category = "Arts",
                    MaxAttendees = 200,
                    CurrentAttendees = 156,
                    Price = 45.00m,
                    ImageUrl = "/images/art-exhibition.jpg",
                    IsActive = true
                },
                new Event
                {
                    Id = 4,
                    Name = "Annual Charity Gala for Education",
                    Description = "Support local education initiatives at our elegant charity gala. Enjoy fine dining, live entertainment, and silent auctions while making a difference in children's lives.",
                    Date = DateTime.Now.AddDays(30),
                    Location = "Grand Ballroom, Chicago, IL",
                    Category = "Charity",
                    MaxAttendees = 300,
                    CurrentAttendees = 198,
                    Price = 125.00m,
                    ImageUrl = "/images/charity-gala.jpg",
                    IsActive = true
                },
                new Event
                {
                    Id = 5,
                    Name = "Fitness and Wellness Expo",
                    Description = "Discover the latest in fitness equipment, healthy nutrition, and wellness practices. Participate in group fitness classes, health screenings, and meet wellness experts.",
                    Date = DateTime.Now.AddDays(12),
                    Location = "Sports Complex, Austin, TX",
                    Category = "Health",
                    MaxAttendees = 400,
                    CurrentAttendees = 145,
                    Price = 35.00m,
                    ImageUrl = "/images/fitness-expo.jpg",
                    IsActive = true
                },
                new Event
                {
                    Id = 6,
                    Name = "Culinary Masterclass Series",
                    Description = "Learn from world-renowned chefs in this intensive culinary workshop. Master advanced cooking techniques, plating presentations, and create restaurant-quality dishes.",
                    Date = DateTime.Now.AddDays(20),
                    Location = "Culinary Institute, Seattle, WA",
                    Category = "Food",
                    MaxAttendees = 50,
                    CurrentAttendees = 47,
                    Price = 225.00m,
                    ImageUrl = "/images/culinary-class.jpg",
                    IsActive = true
                },
                // Expired event for testing expired event handling
                new Event
                {
                    Id = 7,
                    Name = "Past Conference (Expired)",
                    Description = "This event has already occurred and is used for testing expired event handling.",
                    Date = DateTime.Now.AddDays(-5),
                    Location = "Test Location",
                    Category = "Technology",
                    MaxAttendees = 100,
                    CurrentAttendees = 95,
                    Price = 50.00m,
                    ImageUrl = "/images/test.jpg",
                    IsActive = true
                },
                // Event with invalid/missing data for testing error handling
                new Event
                {
                    Id = 8,
                    Name = "",
                    Description = "",
                    Date = DateTime.MinValue,
                    Location = "",
                    Category = "",
                    MaxAttendees = 0,
                    CurrentAttendees = -5,
                    Price = -100.00m,
                    ImageUrl = "",
                    IsActive = true
                }
            };
        }
    }
}