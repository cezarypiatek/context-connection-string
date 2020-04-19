using System.Threading.Tasks;

namespace SampleApp
{
    class SampleJob
    {
        private readonly SampleRepository _sampleRepository;

        public SampleJob(SampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public async Task DoSomethingAsync()
        {
            await _sampleRepository.GetAll();
        }
    }
}