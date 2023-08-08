using PNMT.WebApp.Helper;
using PNMTD.Lib.Models.Poco;
using System.Net.Http.Headers;
using System.Net.Http;
using PNMTD.Models.Responses;
using PNMTD.Models.Poco;
using Microsoft.Extensions.Hosting;
using PNMT.WebApp.Data.Entities;

namespace PNMT.WebApp.Data
{
    public class PNMTDApi
    {
        private readonly HttpClient httpClient;

        public string BaseAddress = "https://localhost:7238";

        public readonly HostApiCrud Hosts;
        public readonly SensorApiCrud Sensors;
        public readonly NotificationRuleApiCrud NotificationRules;
        public readonly EventApiCrud Events;

        public PNMTDApi(HttpClient httpClient, JwtTokenProvider jwtTokenProvider)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri(BaseAddress);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtTokenProvider.JwtToken);
            this.Hosts = new HostApiCrud(httpClient);
            this.Sensors = new SensorApiCrud(httpClient);
            this.NotificationRules = new NotificationRuleApiCrud(httpClient);
            this.Events = new EventApiCrud(httpClient);
        }

        public string GetSensorEventUrl(SensorPoco sensor) {
            return $"{BaseAddress}/event/{sensor.Id}/";
        }

        public string GetSensorEventUrl(SensorPoco sensor, string code, string message)
        {
            return $"{GetSensorEventUrl(sensor)}{code}/{message}";
        }

    }
}
