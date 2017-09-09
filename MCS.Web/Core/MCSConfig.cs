using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCS.Infrastructure.Models;
using MCS.Web.ViewModels;

namespace MCS.Web.Core
{
    public static class MCSConfig
    {
        public static string ConnectionString { get; set; }
        public static string siteUrl { get; set; }
        public static List<DeviceInfo> lstDevice { get; set; }
    }
}
