﻿using Parcs.HostAPI.Models.Domain;
using Parcs.HostAPI.Services.Interfaces;
using Parcs.Net;
using Parcs.Shared.Models;

namespace Parcs.HostAPI.Services
{
    public sealed class HostInfoFactory : IHostInfoFactory
    {
        public IHostInfo Create(Job job, IEnumerable<Daemon> daemons) => new HostInfo(job, daemons);
    }
}