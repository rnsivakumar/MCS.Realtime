// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace MCS.Web
{
    public class UserDetails
    {
        public UserDetails(string connectionId, string  name, int stationid)
        {
            ConnectionId = connectionId;
            Name = name;
            StationId = stationid;
        }

        public string ConnectionId { get; }
        public string Name { get; }
        public int StationId { get; set; }
    }
}
