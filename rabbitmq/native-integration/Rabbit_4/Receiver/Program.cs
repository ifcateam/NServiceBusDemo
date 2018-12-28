﻿using System;
using System.Threading.Tasks;
using NServiceBus;

class Program
{
    static async Task Main()
    {
        Console.Title = "Samples.RabbitMQ.NativeIntegration.Receiver";

        #region ConfigureRabbitQueueName
        var endpointConfiguration = new EndpointConfiguration("Samples.RabbitMQ.NativeIntegration");
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString("host=localhost");
        transport.DelayedDelivery().DisableTimeoutManager();
        #endregion

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}