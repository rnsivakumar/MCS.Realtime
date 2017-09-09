using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MCS.Infrastructure.Models
{
    public partial class TM_SystemMap_Dtls
    {
        [Key]
        public int stationId { get; set; }
        public string stationName { get; set; }
        public string stationAbbr { get; set; }
        public string deviceStateName { get; set; }
        public int totalDevices { get; set; }
        public string color { get; set; }
    }
}
