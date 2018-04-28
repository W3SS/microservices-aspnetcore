using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace EventProcessor.Location.Redis
{
    using System.Linq;

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

        // This is a hack required to get injection working
        // because Steeltoe's redis connector injected the concrete class as binding
        // and not the interface.
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

        private IList<MemberLocation> ConvertRedisValsToLocationList(IEnumerable<RedisValue> vals)
        {
            return vals.Select(t => (string)t).Select(MemberLocation.FromJsonString).ToList();
        }
    }
}