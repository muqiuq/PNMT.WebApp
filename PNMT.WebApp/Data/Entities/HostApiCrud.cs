﻿using PNMTD.Lib.Models.Poco;
using System.Net.Http;

namespace PNMT.WebApp.Data.Entities
{
    public class HostApiCrud : ApiCrud<HostPoco>
    {
        public HostApiCrud(HttpClient httpClient) : base(httpClient, "host")
        {
        }
        public async Task<HostStatePoco> GetHostWithSensors(Guid Id)
        {
            return await httpClient.GetFromJsonAsync<HostStatePoco>($"/hosts/{Id}");
        }
    }
}
