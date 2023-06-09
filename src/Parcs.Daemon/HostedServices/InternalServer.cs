﻿using Microsoft.Extensions.Hosting;
using Parcs.Core.Models;
using Parcs.Core.Services.Interfaces;
using Parcs.Daemon.Services.Interfaces;
using System.Threading.Channels;

namespace Parcs.Daemon.HostedServices
{
    public class InternalServer : IHostedService
    {
        private readonly ChannelReader<InternalChannelReference> _channelReader;
        private readonly IInternalChannelManager _internalChannelManager;
        private readonly IChannelOrchestrator _channelOrchestrator;

        public InternalServer(
            ChannelReader<InternalChannelReference> channelReader, IInternalChannelManager internalChannelManager, IChannelOrchestrator channelOrchestrator)
        {
            _channelReader = channelReader;
            _internalChannelManager = internalChannelManager;
            _channelOrchestrator = channelOrchestrator;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _ = Task.Run(async () => await StartServerAsync(cancellationToken), cancellationToken);

            return Task.CompletedTask;
        }

        private async Task StartServerAsync(CancellationToken cancellationToken)
        {
            await foreach (var internalChannelReference in _channelReader.ReadAllAsync(cancellationToken))
            {
                if (!_internalChannelManager.TryGet(internalChannelReference.Id, out var internalChannelPair))
                {
                    continue;
                }

                _ = Task.Run(async () => await _channelOrchestrator.OrchestrateAsync(internalChannelPair.Item2, cancellationToken), cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}