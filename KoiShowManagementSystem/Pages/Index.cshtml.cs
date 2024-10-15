using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace KoiShowManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Event> Events { get; set; } = new List<Event>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Events = GetEvents();
        }

        private List<Event> GetEvents()
        {
            return new List<Event>
            {
                new Event { Id = 1, Name = "S? ki?n 1", Date = "01/01/2024", Description = "Mô t? s? ki?n 1" },
                new Event { Id = 2, Name = "S? ki?n 2", Date = "02/01/2024", Description = "Mô t? s? ki?n 2" }
            };
        }
    }

    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
    }
}