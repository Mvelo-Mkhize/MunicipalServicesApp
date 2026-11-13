using System;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DateTime today = DateTime.Today;

            EventRepository.AddEvent(new CommunityEvent
            {
                Id = "E001",
                Title = "Fresh Produce Farmers Market",
                Category = "Markets",
                Date = today.AddDays(3).AddHours(9),
                Location = "Town Square",
                Description = "Local farmers sell fresh fruit, vegetables and homemade goods.",
                Priority = EventPriority.Medium
            });

            EventRepository.AddEvent(new CommunityEvent
            {
                Id = "E002",
                Title = "Council Meeting",
                Category = "Utilities",
                Date = today.AddDays(5).AddHours(7),
                Location = "City Hall",
                Description = "Meeting with the city council.",
                Priority = EventPriority.High
            });

            EventRepository.AddEvent(new CommunityEvent
            {
                Id = "A001",
                Title = "Scheduled Water Interruption - Ward 5",
                Category = "Utilities",
                Date = today.AddDays(2).AddHours(8),
                Location = "Ward 5",
                Description = "Planned maintenance on the main water line.",
                Priority = EventPriority.High,
                IsAnnouncement = true
            });

            Application.Run(new FormMain());
        }
    }
}