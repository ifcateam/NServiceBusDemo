﻿using System;
using NServiceBus;

class Program
{
    static void Main()
    {
        Console.Title = "Samples.Scaleout.Worker1";
        #region Workerstartup
        var busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Samples.Scaleout.Worker1");
        busConfiguration.EnlistWithMSMQDistributor();
        #endregion
        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.EnableInstallers();
        using (var bus = Bus.Create(busConfiguration).Start())
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}