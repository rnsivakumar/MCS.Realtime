using System;
using System.Collections.Generic;
using System.Net.Http;
using MCS.Web.ViewModels;
using Microsoft.Extensions.Logging;
using RecurrentTasks;
using MCS.Web.Helpers;
using System.Text;
using MCS.Infrastructure;
using MCS.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace MCS.Web.Core
{
    public class MCSServerEngine : IRunnable
    {
        private ILogger logger;
        private string[] mcolor = { "red", "green", "yellow", "blue", "orange"};
        private IUnitOfWork _unitOfWork;
        public MCSServerEngine(ILogger<MCSServerEngine> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this._unitOfWork = unitOfWork;
        }

        public void Run(ITask currentTask, CancellationToken cancellationToken)
        {
            var msg = string.Format("Run at: {0}", DateTimeOffset.Now);
            logger.LogDebug(msg);
            try
            {
                UpdateEventHistory();
            }
            catch(Exception e)
            {
                logger.LogCritical($"EXCPTION : Run() - Error : {e.Message.ToString()}");
            }
        }

        private async void UpdateEventHistory()
        {
            logger.LogCritical("UpdateEventHistory - Start");
            logger.LogCritical($"UpdateEventHistory - SiteURL : {MCSConfig.siteUrl}");
            //var optionsBuilder = new DbContextOptionsBuilder();
            //optionsBuilder.UseSqlServer("Server=172.16.22.118;Database=MCSDBWeb02;User Id=sa; Password=Pass1234#;MultipleActiveResultSets=true");
            //string strConnString = MCSConfig.ConnectionString;
            //optionsBuilder.UseSqlServer(MCSConfig.ConnectionString);
            //logger.LogCritical($"UpdateEventHistory - DB Conn String : {MCSConfig.ConnectionString}");
            //optionsBuilder.UseSqlServer("Server=.;Database=MCSDBWeb02;Trusted_Connection=True;MultipleActiveResultSets=true");

            //using (var dbContext = new ApplicationDbContext(optionsBuilder.Options))
            //{
            //    lstDevice = await dbContext.Set<TM_DeviceInfo>().FromSql("exec sp_GetDeviceInfo").ToListAsync();
            //}

            //if (null == MCSConfig.lstDevice)
            //{
            //    MCSConfig.lstDevice = new List<TM_DeviceInfo>();
            //    logger.LogCritical("Device is NULL");
            //    MCSConfig.lstDevice.Add(new TM_DeviceInfo() {
            //        deviceId = 1,
            //        deviceType = 59,
            //        deviceSerialNo = 1,
            //        deviceNo = "G01",
            //        deviceState = 1,
            //        stationId = 301,
            //        deviceTypeAbbr = "AG11",
            //        stationAbbr = "BGD"
            //    });
            //}

           
            using (var client = new HttpClient())
            {
                //logger.LogCritical("Reading no.of devices");

                //int noOfDevices = MCSConfig.lstDevice.Count;
                //logger.LogCritical("Reading no.of devices - end");

                int eventId = new Random().Next(0, 20);
                int eventState = new Random().Next(0, 1);
                int stationId = new Random().Next(1, 10);
                int color = new Random().Next(0, 4);
                DeviceInfo device = null;

                //logger.LogCritical($"Device Count : {MCSConfig.lstDevice.Count}");
                //if (MCSConfig.lstDevice.Count > 0)
                //{
                //    device = MCSConfig.lstDevice[new Random().Next(1, noOfDevices) - 1];
                //}
               
                if (null == device)
                {
                    device = new DeviceInfo() {
                        deviceId = 1,
                        deviceType = 59,
                        deviceSerialNo = 1,
                        deviceNo = "G01",
                        deviceState = 1,
                        stationId = 301,
                        deviceTypeAbbr = "AG11",
                        stationAbbr = "BGD"
                    };
                    
                    logger.LogCritical("Device is NULL, but set to Default Value");
                    //return;
                }

                logger.LogCritical("Creating EventHistoryViewModel");

                EventHistoryViewModel eventVM = new EventHistoryViewModel()
                {
                    stationId = device.stationId, //
                    stationAbbr = device.stationAbbr,
                    eventId = eventId,
                    deviceId = device.deviceId,
                    eventDesc = string.Format("{0} - Event Description", eventId),
                    eventDateTime = DateTime.Now,
                    eventState = string.Format("{0} - {1}", eventState, eventState == 1 ? "Set" : "Clear"),
                    equipmentNo = device.deviceNo, // string.Format("G{0}", deviceNo),
                    eventColor = mcolor[color],
                    deviceType = device.deviceTypeAbbr,
                    severity = "0 - Normal",
                    historyId = Guid.NewGuid().ToString()
                };

                logger.LogCritical("Posting...");

                HttpContent contentPost = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(eventVM), Encoding.UTF8, "application/json");

                //client.BaseAddress = new Uri("https://localhost:44360");
                //client.BaseAddress = new Uri("http://localhost:5000");
                client.BaseAddress = new Uri(MCSConfig.siteUrl); // "https://localhost:44360");
                                                                 //client.BaseAddress = new Uri("https://172.16.22.118");

                logger.LogCritical($"client.PutAsync... {client.BaseAddress}");

                var response = await client.PutAsync($"/api/mcs/eventhistory/{device.stationId}", contentPost); //.ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()); ;
                logger.LogCritical($"UpdateEventHistory - End, Response : {response.ToString()}");
            }
            
            logger.LogCritical("UpdateEventHistory - End");
        }
    }
}