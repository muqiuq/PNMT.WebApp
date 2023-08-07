using PNMT.WebApp.Helper;
using PNMTD.Lib.Models.Poco;
using System.Net.Http.Headers;
using System.Net.Http;
using PNMTD.Models.Responses;
using PNMTD.Models.Poco;
using Microsoft.Extensions.Hosting;

namespace PNMT.WebApp.Data
{
    public class PNMTDApi
    {
        private readonly HttpClient httpClient;

        public string BaseAddress = "https://localhost:7238";

        public PNMTDApi(HttpClient httpClient, JwtTokenProvider jwtTokenProvider)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri(BaseAddress);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtTokenProvider.JwtToken);
        }

        public async Task<SensorPoco> GetSensor(Guid Id)
        {
            return await httpClient.GetFromJsonAsync<SensorPoco>($"/sensor/{Id}");
        }

        public async Task<HostStatePoco> GetHostWithSensors(Guid Id)
        {
            return await httpClient.GetFromJsonAsync<HostStatePoco>($"/hosts/{Id}");
        }

        public async Task<List<HostPoco>> GetHosts()
        {
            return await httpClient.GetFromJsonAsync<List<HostPoco>>("/host");
        }

        public async Task DeleteHost(Guid Id)
        {
            var result = await httpClient.DeleteFromJsonAsync<DefaultResponse>($"/host/{Id}");

            if (!result.Success)
            {
                throw new PNMTDApiException($"Delete Error");
            }
        }

        public async Task<HostPoco> GetHost(Guid Id)
        {
            return await httpClient.GetFromJsonAsync<HostPoco>($"/host/{Id}");
        }

        public async Task AddHost(HostPoco host)
        {
            var result = await httpClient.PostAsJsonAsync<HostPoco>("/host", host);
            var response = await result.Content.ReadFromJsonAsync<DefaultResponse>();

            if (!result.IsSuccessStatusCode || !response.Success) {
                throw new PNMTDApiException($"HTTP {result.StatusCode}");
            }
        }

        public async Task SaveHost(HostPoco host)
        {
            var result = await httpClient.PutAsJsonAsync<HostPoco>("/host", host);
            var response = await result.Content.ReadFromJsonAsync<DefaultResponse>();

            if (!result.IsSuccessStatusCode || !response.Success)
            {
                throw new PNMTDApiException($"HTTP {result.StatusCode}");
            }
        }
    }
}
