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
    public class SystemMapViewModel
    {
        public int stationId { get; set; }
        public string stationName { get; set; }
        public string stationAbbr { get; set; }
        public string deviceStateName { get; set; }
        public int totalDevices { get; set; }
        public string color { get; set; }
    }
}
