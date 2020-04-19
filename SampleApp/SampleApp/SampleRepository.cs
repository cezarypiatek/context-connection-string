using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SampleApp
{
    public class SampleRepository
    {
        private readonly IConnectionStringProvider _connectionStringProvider;
        private readonly ILogger<SampleRepository> _logger;

        public SampleRepository(IConnectionStringProvider connectionStringProvider, ILogger<SampleRepository> logger)
        {
            _connectionStringProvider = connectionStringProvider;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<SampleEntity>> GetAll()
        {
            var connectionString = _connectionStringProvider.GetConnectionString();
            _logger.LogWarning($"Current connection string {connectionString}");
            //TODO: create connection and execute query
            return Array.Empty<SampleEntity>();
        }
    }
}