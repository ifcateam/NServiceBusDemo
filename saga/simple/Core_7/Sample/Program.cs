using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NServiceBus;
using NServiceBus.Persistence.Sql;

class Program
{
    static async Task Main()
    {
        Console.Title = "Samples.SimpleSaga rabbitmq&mysql";
        var endpointConfiguration = new EndpointConfiguration("quarrierSamples.SimpleSaga");

        #region Persistence

        

        var sqlPersistence =
            endpointConfiguration.UsePersistence<SqlPersistence>();

        var connection = $"server=192.168.137.51;user=root;database=new_schema_Test;port=3306;password=123456;AllowUserVariables=True;AutoEnlist=false";
        sqlPersistence.SqlDialect<SqlDialect.MySql>();
        sqlPersistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new MySqlConnection(connection);
            });
        
        
        var subscriptions = sqlPersistence.SubscriptionSettings();
        subscriptions.CacheFor(TimeSpan.FromMinutes(1));

        endpointConfiguration
            .UsePersistence<InMemoryPersistence, StorageType.Subscriptions>();
        

        #endregion


        #region Transport
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();

        transport.ConnectionString("host=192.168.137.51;" +
                                   "username=admin;" +
                                   "password=admin");
        transport.UseConventionalRoutingTopology();

        #endregion

        endpointConfiguration.EnableInstallers();

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);

        Console.WriteLine();
        Console.WriteLine("Storage locations:");
        Console.WriteLine($"Learning Persister: {LearningLocationHelper.SagaDirectory}");
        Console.WriteLine($"Learning Transport: {LearningLocationHelper.TransportDirectory}");

        Console.WriteLine();
        Console.WriteLine("Press 'Enter' to send a StartOrder message");
        Console.WriteLine("Press any other key to exit");

        while (true)
        {
            Console.WriteLine();
            if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                break;
            }
            var orderId = Guid.NewGuid();
            var startOrder = new StartOrder
            {
                OrderId = orderId
            };
            await endpointInstance.SendLocal(startOrder)
                .ConfigureAwait(false);
            Console.WriteLine($"Sent StartOrder with OrderId {orderId}.");
        }

        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}