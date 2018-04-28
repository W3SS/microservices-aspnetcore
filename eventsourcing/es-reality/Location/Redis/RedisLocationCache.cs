using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace RealityService.Location.Redis
{
    public class RedisLocationCache : ILocationCache
    {
        private ILogger logger;

        private readonly IConnectionMultiplexer connection;

        public RedisLocationCache(ILogger<RedisLocationCache> logger,
            IConnectionMultiplexer connectionMultiplexer)
        {
            this.logger = logger;
            this.connection = connectionMultiplexer;

            logger.LogInformation($"Using redis location cache - {connectionMultiplexer.Configuration}");
        }

        public RedisLocationCache(ILogger<RedisLocationCache> logger,
            ConnectionMultiplexer connectionMultiplexer) : this(logger, (IConnectionMultiplexer)connectionMultiplexer)
        {

        }

        public IList<MemberLocation> GetMemberLocations(Guid teamId)
        {
            var db = this.connection.GetDatabase();

            var vals = db.HashValues(teamId.ToString());

            return this.ConvertRedisValsToLocationList(vals);
        }

        public void Put(Guid teamId, MemberLocation memberLocation)
        {
            var db = this.connection.GetDatabase();

            db.HashSet(teamId.ToString(), memberLocation.MemberId.ToString(), memberLocation.ToJsonString());
        }

        public MemberLocation Get(Guid teamId, Guid memberId)
        {
            var db = this.connection.GetDatabase();

            var value = (string)db.HashGet(teamId.ToString(), memberId.ToString());
            MemberLocation ml = MemberLocation.FromJsonString(value);
            return ml;
        }

        private IList<MemberLocation> ConvertRedisValsToLocationList(RedisValue[] vals)
        {
            List<MemberLocation> memberLocations = new List<MemberLocation>();

            foreach (var t in vals)
            {
                var val = (string)t;
                MemberLocation ml = MemberLocation.FromJsonString(val);
                memberLocations.Add(ml);
            }

            return memberLocations;
        }
    }

    public static class RedisExtensions
    {
        public static IServiceCollection AddRedisConnectionMultiplexer(this IServiceCollection services,
            IConfiguration config)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var redisConfig = config.GetSection("redis:configstring").Value;

            services.AddSingleton(typeof(IConnectionMultiplexer), ConnectionMultiplexer.ConnectAsync(redisConfig).Result);
            return services;
        }
    }
}