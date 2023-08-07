using PNMTD.Models.Poco;

namespace PNMT.WebApp.Data.Entities
{
    public class SensorApiCrud : ApiCrud<SensorPoco>
    {
        public SensorApiCrud(HttpClient httpClient) : base(httpClient, "sensor")
        {
        }

    }
}
