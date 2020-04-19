using System;

namespace SampleApp
{
    class ContextCleaner : IDisposable
    {
        private readonly Action _cleanUpAction;

        public ContextCleaner(Action cleanUpAction)
        {
            _cleanUpAction = cleanUpAction;
        }

        public void Dispose()
        {
            _cleanUpAction?.Invoke();
        }
    }
}