namespace MCS.Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TM_DeviceInfo
    {
        [Key]
        public long deviceId { get; set; }
        public int deviceType { get; set; }
        public long deviceSerialNo { get; set; }
        public string deviceNo { get; set; }
        public int? deviceState { get; set; }
        public int stationId { get; set; }
        public string deviceTypeAbbr { get; set; }
        public string stationAbbr { get; set; }
    }
}
