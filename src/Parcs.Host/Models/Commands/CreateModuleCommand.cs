﻿using MediatR;
using Parcs.HostAPI.Models.Responses;

namespace Parcs.HostAPI.Models.Commands
{
    public class CreateModuleCommand : IRequest<CreateModuleCommandResponse>
    {
        public IEnumerable<IFormFile> BinaryFiles { get; set; }
    }
}