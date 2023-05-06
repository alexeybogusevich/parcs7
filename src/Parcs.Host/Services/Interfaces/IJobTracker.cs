﻿namespace Parcs.HostAPI.Services.Interfaces
{
    public interface IJobTracker
    {
        void StartTracking(long jobId);
        bool TryGetCancellationToken(long jobId, out CancellationToken cancellationToken);
        bool CancelAndStopTrackning(long jobId);
        bool StopTracking(long jobId);
    }
}