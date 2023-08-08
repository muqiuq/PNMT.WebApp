using PNMTD.Lib.Models.Poco;
using System.Net.Http;

namespace PNMT.WebApp.Data.Entities
{
    public class EventApiCrud
    {
        private readonly HttpClient httpClient;

        public EventApiCrud(HttpClient httpClient) 
        {
            this.httpClient = httpClient;
        }
        public async Task<List<EventEntityPoco>> GetEventsForSensor(Guid Id)
        {
            return await httpClient.GetFromJsonAsync<List<EventEntityPoco>>($"/events/sensor/{Id}");
        }

    }
}
