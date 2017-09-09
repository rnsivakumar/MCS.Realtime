using MCS.Web.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MCS.Web.Controllers
{
    public abstract class ApiHubController<T> : Controller
        where T : Hub
    {
        protected HubLifetimeManager<HubEventHistory> _hub;
        protected ApiHubController(HubLifetimeManager<HubEventHistory> signalRConnectionManager) 
        {
            _hub = signalRConnectionManager;
        }
    }
}