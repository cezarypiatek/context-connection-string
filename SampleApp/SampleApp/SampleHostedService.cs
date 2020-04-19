using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SampleApp
{
    class SampleHostedService : IHostedService
    {

        private readonly IContextConnectionStringProvider _contextConnectionStringProvider;
        private readonly SampleJob _sampleJob;
        private Task _mainJobTask;
        private CancellationTokenSource _mainJobCancellation;

        public SampleHostedService(IContextConnectionStringProvider contextConnectionStringProvider, SampleJob sampleJob)
        {
            _contextConnectionStringProvider = contextConnectionStringProvider;
            _sampleJob = sampleJob;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this._mainJobCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            this._mainJobTask = Task.Run(async () =>
            {
                foreach (var connectionString in new []{"ConnectionString1", "ConnectionString2", "ConnectionString3" })
                {
                    using (_contextConnectionStringProvider.SetConnectionString(connectionString))
                    {
                        await this._sampleJob.DoSomethingAsync();
                    }
                }
            }, _mainJobCancellation.Token);
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_mainJobTask.IsCompleted == false)
            {
                _mainJobCancellation.Cancel();
                await Task.Run(async () =>
                {
                    while (_mainJobTask.IsCompleted == false)
                    {
                        await Task.Delay(200, cancellationToken);
                    }
                }, cancellationToken);
            }
        }
    }
}