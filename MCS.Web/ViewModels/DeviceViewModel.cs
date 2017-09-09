
using System;
using System.Linq;

namespace MCS.Web.ViewModels
{
    public class DeviceViewModel
    {
        public int deviceId { get; set; }
        public int stationId { get; set; }
        public int deviceType { get; set; }
        public long deviceSerialNo { get; set; }
        public string deviceNo { get; set; }
        public bool isInUse { get; set; }
        public System.DateTime lastUpdateTime { get; set; }
    }

    public class DeviceInfo
    {
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
