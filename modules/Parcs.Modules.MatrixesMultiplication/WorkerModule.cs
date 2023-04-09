﻿using Parcs.Modules.MatrixesMultiplication.Models;
using Parcs.Net;

namespace Parcs.Modules.MatrixesMultiplication
{
    public class WorkerModule : IWorkerModule
    {
        public async Task RunAsync(IChannel channel, CancellationToken cancellationToken = default)
        {
            var matrixA = await channel.ReadObjectAsync<Matrix>();
            var matrixB = await channel.ReadObjectAsync<Matrix>();

            var matrixAB = matrixA.MultiplyBy(matrixB, cancellationToken);

            await channel.WriteObjectAsync(matrixAB);
        }
    }
}