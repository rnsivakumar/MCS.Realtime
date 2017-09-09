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

namespace MCS.Infrastructure.Repositories.Interfaces
{
    public interface IEventHistoryRepository : IRepository<TX_Event_History>
    {
        IEnumerable<TX_Event_History> GetTopActiveEventHistory(int count);
        IEnumerable<TX_Event_History> GetAllEventHistoryData();
        IEnumerable<TM_Event_History_Dtls> GetAllEventHistoryDtls(int stationId, string deviceNo);
        IEnumerable<TM_SystemMap_Dtls> GetSystemMapDtls();
        IList<TM_DeviceInfo> GetDevicesInfo();
    }
}
