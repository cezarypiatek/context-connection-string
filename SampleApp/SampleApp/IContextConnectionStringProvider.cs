namespace SampleApp
{
    internal interface IContextConnectionStringProvider: IConnectionStringProvider
    {
        ContextCleaner SetConnectionString(string connectionString);
    }
}