// ======================================



// 

// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MCS.Infrastructure;
using MCS.Web.ViewModels;
using AutoMapper;
using MCS.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using MCS.Web.Helpers;
using MCS.Web.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace MCS.Web.Controllers
{
    [Route("api/[controller]")]
    public class MCSController : ApiHubController<HubEventHistory>
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        public MCSController(HubLifetimeManager<HubEventHistory> signalRConnectionManager,
            IUnitOfWork unitOfWork, ILogger<MCSController> logger) : base(signalRConnectionManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        [HttpGet("email")]
        public async Task<string> Email()
        {
            string recepientName = "QickApp Tester"; //         <===== Put the recepient's name here
            string recepientEmail = "test@ebenmonney.com"; //   <===== Put the recepient's email here

            string message = EmailTemplates.GetTestEmail(recepientName, DateTime.UtcNow);

            (bool success, string errorMsg) response = await EmailSender.SendEmailAsync(recepientName, recepientEmail, "Test Email from MCS.Web", message);

            if (response.success)
                return "Success";

            return "Error: " + response.errorMsg;
        }
        
        [HttpGet("eventhistory/all/{id}")]
        public IActionResult GetEventHistory(int id)
        {
            //var allEventHistory = _unitOfWork.EventHistory.GetAllEventHistoryData();
            var allEventHistory = _unitOfWork.EventHistory.GetAllEventHistoryDtls(id, "");
            return Json(Mapper.Map<IEnumerable<EventHistoryViewModel>>(allEventHistory));
        }

        [HttpGet]
        public IActionResult GetParameterSummary(int stationId)
        {
            var allEventHistory = _unitOfWork.EventHistory.GetAllEventHistoryDtls(stationId, "");
            return Json(Mapper.Map<IEnumerable<EventHistoryViewModel>>(allEventHistory));
        }

        [HttpGet]
        public IActionResult GetCardkeySummary(int stationId)
        {
            var allEventHistory = _unitOfWork.EventHistory.GetAllEventHistoryDtls(stationId, "");
            return Json(Mapper.Map<IEnumerable<EventHistoryViewModel>>(allEventHistory));
        }

        [HttpGet]
        public IActionResult GetFirmwareSummary(int stationId)
        {
            var allEventHistory = _unitOfWork.EventHistory.GetAllEventHistoryDtls(stationId, "");
            return Json(Mapper.Map<IEnumerable<EventHistoryViewModel>>(allEventHistory));
        }

        [HttpGet]
        public IActionResult GetCommandHistory(int stationId)
        {
            var allEventHistory = _unitOfWork.EventHistory.GetAllEventHistoryDtls(stationId, "");
            return Json(Mapper.Map<IEnumerable<EventHistoryViewModel>>(allEventHistory));
        }

        [HttpGet]
        public IActionResult GetStationSummary(int stationId)
        {
            var allEventHistory = _unitOfWork.EventHistory.GetAllEventHistoryDtls(stationId, "");
            return Json(Mapper.Map<IEnumerable<EventHistoryViewModel>>(allEventHistory));
        }

        [HttpGet("systemmap/all")]
        public IActionResult GetSystemMapSummary()
        {
            var systemMapSummary = _unitOfWork.EventHistory.GetSystemMapDtls();
            return Json(Mapper.Map<IEnumerable<SystemMapViewModel>>(systemMapSummary));
        }

        //[HttpPut("eventhistory/{stationId}")]
        //public async void Put(int stationId, [FromBody]EventHistoryViewModel value)
        //{
        //    var strHistory = $@"Station : {stationId}, Device : {value.DeviceNo}, Event : {value.EventDesc}, Time : {value.EventDateTime.ToString("dd-MM-yyyy HH:mm:ss")}";
        //    await Clients.Group(stationId.ToString()).UpdateEventHistory(strHistory);
        //}

        [HttpPut("eventhistory/{stationId}")]
        public async void Put(int stationId, [FromBody]EventHistoryViewModel value)
        {
            //var strHistory = $@"Station : {stationId}, Device : {value.DeviceNo}, Event : {value.EventDesc}, Time : {value.EventDateTime.ToString("dd-MM-yyyy HH:mm:ss")}";
            //await Clients.Group(stationId.ToString()).UpdateEventHistory(Json(Mapper.Map<EventHistoryViewModel>(value)));
            try
            {

           
            _unitOfWork.EventHistory.Add(new TX_Event_History() {
                deviceId = value.deviceId,
                eventId = value.eventId,
                eventDateTime = value.eventDateTime,
                eventState = value.eventStateVal,
                updateTime = DateTime.Now,
                stationId = value.stationId});
                int iResult = _unitOfWork.SaveChanges();
                if (0 == iResult)
                {
                    _logger.LogCritical($"DBUpdate Event - Event : {value.stationId}");
                }
            }catch(Exception ee)
            {
                _logger.LogCritical($"DBUpdate Event - Error : {ee.Message.ToString()}");
            }

            await _hub.InvokeGroupAsync(stationId.ToString(), "updateEventHistory", new object[] { Json(value) });
            //await Clients.Group(stationId.ToString()).UpdateEventHistory(Json(value));
        }

        [HttpPut("cardkey/{stationId}")]
        public async void Put(int stationId, [FromBody]CardkeyViewModel value)
        {
            await _hub.InvokeGroupAsync(stationId.ToString(), "addCardkey", new object[] { Json(value) });
        }
    }
}
