using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroSlope.Packages.DotNet.Exceptions;

namespace ZeroSlope.Packages.DotNet.Redis.Installers
{
    public class RedisInstaller
    {
        private readonly string _host;
        private readonly int _port;
        private readonly int _databaseId;

        public RedisInstaller(string host, int port, int databaseId)
        {
            _host = host;
            _port = port;
            _databaseId = databaseId;
        }

        public void Install(IServiceCollection serviceCollection)
        {
            try
            {
                var connection = ConnectionMultiplexer.Connect($"{_host}:{_port}");
                var db = connection.GetDatabase(_databaseId);
                serviceCollection.AddSingleton<IConnectionMultiplexer>(connection);
                serviceCollection.AddSingleton<IDatabase>(db);
            }
            catch (Exception ex)
            {
                throw new HandledException(ExceptionType.Service, ex.Message);
            }
        }
    }
}
