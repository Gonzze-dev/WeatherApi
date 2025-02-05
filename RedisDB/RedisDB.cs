using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace RedisDB
{
    public class RedisDBConnection
    {
        readonly IConfiguration _configuration;

        readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        public ConnectionMultiplexer Connection
        {
            get => _lazyConnection.Value;
        }

        public RedisDBConnection(
            IConfiguration configuration
        )
        {
            _configuration = configuration;

            var END_POINT = _configuration["REDIS:END_POINT"] ?? "localhost";
            var PORT = int.TryParse(_configuration["REDIS:PORT"], out var port) ? port : 6000;
            var PASSWORD = _configuration["REDIS:PASSWORD"] ??  "1234";

            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>

                ConnectionMultiplexer.Connect(new ConfigurationOptions()
                {
                    EndPoints = { { END_POINT, PORT } },
                    Password = PASSWORD,
                    AbortOnConnectFail = false,
                })
            );
        }
    }
}
