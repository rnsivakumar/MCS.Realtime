using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MCS.Infrastructure.Models
{
    public partial class TM_Event_History_Dtls
    {
        [Key]
        public System.Guid historyId { get; set; }
        public int stationId { get; set; }
        public string stationAbbr { get; set; }
        public string equipmentNo { get; set; }
        public System.DateTime eventDateTime { get; set; }
        public int eventId { get; set; }
        public string deviceType { get; set; }
        public string eventDesc { get; set; }
        public string eventState { get; set; }
        public string severity { get; set; }
        public string eventColor { get; set; }
    }
}
