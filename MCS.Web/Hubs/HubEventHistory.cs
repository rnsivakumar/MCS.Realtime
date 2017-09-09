
using System.Threading.Tasks;
using MCS.Web.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace MCS.Web.Hubs
{
    

   //public class HubEventHistory : HubWithPresence //: Hub
    public class HubEventHistory : HubWithPresence //: Hub
    {
        public HubEventHistory(IUserTracker<HubEventHistory> userTracker)
            : base(userTracker)
        {
        }

        public override async Task OnConnectedAsync()
        {
            //await Clients.Client(Context.ConnectionId).InvokeAsync("SetUsersOnline", await GetUsersOnline());
            await Clients.Client(Context.ConnectionId).InvokeAsync("setConnectionId", Context.ConnectionId);
            await base.OnConnectedAsync();
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
