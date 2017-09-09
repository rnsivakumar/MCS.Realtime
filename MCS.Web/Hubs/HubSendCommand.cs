
using Microsoft.AspNetCore.SignalR;

namespace MCS.Web.Hubs
{
    public class HubSendCommand : Hub
    {
        public void Join()
        {
            //Clients.All.join($"{Context.ConnectionId} has been joined room");
        }

        public void Subscribe(int stationId)
        {
            Groups.AddAsync(Context.ConnectionId, stationId.ToString());
        }

        public void UnSubscribe(int stationId)
        {
            Groups.RemoveAsync(Context.ConnectionId, stationId.ToString());
        }
    }
}
