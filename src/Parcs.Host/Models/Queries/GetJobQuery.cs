﻿using MediatR;
using Parcs.HostAPI.Models.Responses;

namespace Parcs.HostAPI.Models.Queries
{
    public class GetJobQuery : IRequest<GetJobQueryResponse>
    {
        public GetJobQuery()
        {
        }

        public GetJobQuery(Guid jobId)
        {
            JobId = jobId;
        }

        public Guid JobId { get; set; }
    }
}