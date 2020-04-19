using System;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace SampleApp
{
    class ContextConnectionStringProvider : IContextConnectionStringProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        internal AsyncLocal<string> ConnectionStringStorage { get; } = new AsyncLocal<string>();


        public ContextConnectionStringProvider(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                //return _configuration.GetConnectionString("DatabaseForHttpRequests");
                return "DatabaseForHttpRequests";
            }

            var value = ConnectionStringStorage.Value;
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException("Connection string is not set for current context");
            }

            return value;
        }

        public ContextCleaner SetConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(ConnectionStringStorage.Value) == false)
            {
                throw new InvalidOperationException("Connection string is already set for current context");
            }

            ConnectionStringStorage.Value = connectionString;
            return new ContextCleaner(() => ConnectionStringStorage.Value = null);
        }
    }
}