using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using LocationReporter.Models;

namespace LocationReporter.Services
{
    public class HttpTeamServiceClient : ITeamServiceClient
    {        
        private readonly ILogger logger;

        private readonly HttpClient httpClient;

        public HttpTeamServiceClient(
            IOptions<TeamServiceOptions> serviceOptions,
            ILogger<HttpTeamServiceClient> logger)
        {
            this.logger = logger;
               
            var url = serviceOptions.Value.Url;

            logger.LogInformation("Team Service HTTP client using URL {0}", url);

            this.httpClient = new HttpClient { BaseAddress = new Uri(url) };
        }
        public Guid GetTeamForMember(Guid memberId)
        {
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = this.httpClient.GetAsync(string.Format("/members/{0}/team", memberId)).Result;

            if (response.IsSuccessStatusCode) {
                var json = response.Content.ReadAsStringAsync().Result;
                var teamIdResponse = JsonConvert.DeserializeObject<TeamIdResponse>(json);
                return teamIdResponse.TeamID;
            }

            return Guid.Empty;
        }
    }

    public class TeamIdResponse
    {
        public Guid TeamID { get; set; }
    }
}