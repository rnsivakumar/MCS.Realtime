using System;
using System.Linq;


namespace MCS.Web.ViewModels
{
    public class EventViewModel
    {
        public int eventId { get; set; }
        public string eventDescription { get; set; }
        public int eventType { get; set; }
        public int eventSeverity { get; set; }
        public bool isInUsse { get; set; }
    }
}
