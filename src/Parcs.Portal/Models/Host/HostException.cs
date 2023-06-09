﻿using System.Runtime.Serialization;

namespace Parcs.Portal.Models.Host
{
    [Serializable]
    public class HostException : Exception
    {
        public HostException()
        {
        }

        public HostException(string message)
            : base(message)
        {
        }

        public HostException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected HostException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public HostException(ExtendedProblemDetails problemDetails)
        {
            ProblemDetails = problemDetails;
        }

        public ExtendedProblemDetails ProblemDetails { get; set; }
    }
}