﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Parcs.Daemon.Extensions;
using Parcs.Daemon.HostedServices;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<InternalServer>();
        services.AddHostedService<TcpServer>();
        services.AddApplicationServices();
        services.AddApplicationOptions(hostContext.Configuration);
        services.AddApplicationLogging(hostContext.Configuration);
        services.AddHttpClients(hostContext.Configuration);
    })
    .Build().RunAsync();