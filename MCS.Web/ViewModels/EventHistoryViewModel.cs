
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MCS.Web.ViewModels
{
    [Serializable]
    public class EventHistoryViewModel
    {
        public string historyId { get; set; } //may need if want to display all station summary
        public int stationId { get; set; } //may need if want to display all station summary
        public string stationAbbr { get; set; }
        public string equipmentNo { get; set; }
        public DateTime eventDateTime { get; set; }
        public int eventId { get; set; }
        public long deviceId { get; set; }
        public string deviceType { get; set; }
        public string eventDesc { get; set; }
        public string eventState { get; set; }
        public bool eventStateVal { get; set; }
        public string severity { get; set; }
        public string eventColor { get; set; }
    }

    public class EventHistoryViewModelValidator : AbstractValidator<EventHistoryViewModel>
    {
        public EventHistoryViewModelValidator()
        {
            RuleFor(register => register.equipmentNo).NotEmpty().WithMessage("Device No cannot be empty");
            RuleFor(register => register.eventDesc).NotEmpty().WithMessage("Event Description cannot be empty");
        }
    }
}
