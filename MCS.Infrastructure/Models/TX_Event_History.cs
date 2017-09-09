//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MCS.Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class TX_Event_History
    {
        [Key]
        public System.Guid historyId { get; set; }
        public long deviceId { get; set; }
        public int eventId { get; set; }
        public System.DateTime eventDateTime { get; set; }
        public bool eventState { get; set; }
        public System.DateTime updateTime { get; set; }
        public int stationId { get; set; }
        public virtual TP_Device TP_Device { get; set; }
        public virtual TP_Events TP_Events { get; set; }
        //public virtual TX_Event_History TX_Event_History1 { get; set; }
        //public virtual TX_Event_History TX_Event_History2 { get; set; }
    }
}
