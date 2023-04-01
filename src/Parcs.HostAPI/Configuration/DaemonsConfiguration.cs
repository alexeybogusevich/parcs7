﻿using Parcs.Shared.Models;

namespace Parcs.HostAPI.Configuration
{
    public class DaemonsConfiguration
    {
        public const string SectionName = "Daemons";

        public IEnumerable<Daemon> PreconfiguredInstances { get; set; }
    }
}