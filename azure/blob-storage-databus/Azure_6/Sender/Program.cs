﻿using System;
using NServiceBus;
using NServiceBus.DataBus;

class Program
{
    static void Main()
    {
        Console.Title = "Samples.AzureBlobStorageDataBus.Sender";
        var busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Samples.AzureBlobStorageDataBus.Sender");
        busConfiguration.UseSerialization<JsonSerializer>();

        #region ConfiguringDataBusLocation

        var dataBus = busConfiguration.UseDataBus<AzureDataBus>();
        dataBus.ConnectionString("UseDevelopmentStorage=true");

        #endregion

        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.EnableInstallers();
        using (var bus = Bus.Create(busConfiguration).Start())
        {
            Run(bus);
        }
    }

    static void Run(IBus bus)
    {
        Console.WriteLine("Press 'Enter' to send a large message (>4MB)");
        Console.WriteLine("Press any other key to exit");

        while (true)
        {
            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.Enter)
            {
                SendMessageLargePayload(bus);
            }
            else
            {
                return;
            }
        }
    }

    static void SendMessageLargePayload(IBus bus)
    {
        Console.WriteLine("Sending message...");

        #region SendMessageLargePayload

        var message = new MessageWithLargePayload
        {
            Description = "This message contains a large payload that will be sent on the Azure data bus",
            LargePayload = new DataBusProperty<byte[]>(new byte[1024*1024*5]) // 5MB
        };
        bus.Send("Samples.AzureBlobStorageDataBus.Receiver", message);

        #endregion

        Console.WriteLine("Message sent.");
    }
}