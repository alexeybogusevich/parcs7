﻿using MediatR;
using Parcs.Host.Models.Responses;

namespace Parcs.Host.Models.Queries
{
    public class GetAllJobsQuery : IRequest<IEnumerable<GetJobQueryResponse>>
    {
        public GetAllJobsQuery()
        {
        }
    }
}