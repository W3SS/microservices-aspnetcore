using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ProximityMonitor.TeamService
{
    public class HttpTeamServiceClient : ITeamServiceClient
    {
        private readonly TeamServiceOptions teamServiceOptions;

        private readonly ILogger logger;

        private readonly HttpClient httpClient;
        
        public HttpTeamServiceClient(ILogger<HttpTeamServiceClient> logger,
            IOptions<TeamServiceOptions> serviceOptions)
        {
            this.logger = logger;               
            this.teamServiceOptions = serviceOptions.Value;
            
            logger.LogInformation("Team Service HTTP client using URL {0}", this.teamServiceOptions.Url);

            this.httpClient = new HttpClient { BaseAddress = new Uri(this.teamServiceOptions.Url) };
        }

        public Team GetTeam(Guid teamId)
        {
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = this.httpClient.GetAsync(String.Format("/teams/{0}", teamId)).Result;

            Team teamResponse = null;
            if (response.IsSuccessStatusCode) {
                var json = response.Content.ReadAsStringAsync().Result;
                teamResponse = JsonConvert.DeserializeObject<Team>(json);                
            }
            return teamResponse;
        }

        public Member GetMember(Guid teamId, Guid memberId)
        {
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = this.httpClient.GetAsync(String.Format("/teams/{0}/members/{1}", teamId, memberId)).Result;

            Member memberResponse = null;
            if (response.IsSuccessStatusCode) {
                var json = response.Content.ReadAsStringAsync().Result;
                memberResponse = JsonConvert.DeserializeObject<Member>(json);
            }
            return memberResponse;
        }
    }
}