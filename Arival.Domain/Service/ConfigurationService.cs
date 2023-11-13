using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arival.Domain.IService;
using Microsoft.Extensions.Configuration;

namespace Arival.Domain.Service
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;
        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TimeSpan CodeExpirationTimestamp =>
            TimeSpan.Parse(_configuration.GetValue<string>("OtpSetting:CodeExpirationTimestamp"));

        public int MaximumActiveCodePerPhone =>
            int.Parse(_configuration.GetValue<string>("OtpSetting:MaximumActiveCodePerPhone"));
    }
}
