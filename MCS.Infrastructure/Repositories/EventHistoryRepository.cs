

// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using MCS.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MCS.Infrastructure.Repositories.Interfaces;
using System.Data.Common;

namespace MCS.Infrastructure.Repositories
{
    public class EventHistoryRepository : Repository<TX_Event_History>, IEventHistoryRepository
    {
        public EventHistoryRepository(DbContext context) : base(context)
        { }

        public IEnumerable<TX_Event_History> GetTopActiveEventHistory(int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TX_Event_History> GetAllEventHistoryData()
        {
            return appContext.TX_Event_History
                //.Include(c => c.TP_Device).ThenInclude(o => o.TP_Station).ThenInclude(d => d.TP_Device)
                //.Include(c => c.TP_Events).ThenInclude(o => o.TP_Event_Severity)
                .OrderByDescending(c => c.eventDateTime)
                .ToList();
        }

        public IEnumerable<TM_Event_History_Dtls> GetAllEventHistoryDtls(int stationId, string deviceNo)
        {
            //var value = appContext.Set<TM_Event_History_Dtls>().FromSql("exec sp_GetEventHistory @stationId = {0}, @deviceNo = {1}", stationId, deviceNo).ToList();
            var value = appContext.Set<TM_Event_History_Dtls>().FromSql($"exec sp_GetEventHistory {stationId}, \'{deviceNo}\'").ToList();
            return value;
        }

        public IEnumerable<TM_SystemMap_Dtls> GetSystemMapDtls()
        {
            var value = appContext.Set<TM_SystemMap_Dtls>().FromSql("exec sp_GetSystemMapInfo").ToList();
            return value;
        }

        public IList<TM_DeviceInfo> GetDevicesInfo()
        {
            var value = appContext.Set<TM_DeviceInfo>().FromSql("exec sp_GetDeviceInfo").ToList();
            return value;
        }
        
        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
