﻿using MediatR;
using Parcs.Host.Models.Commands.Base;

namespace Parcs.Host.Models.Commands
{
    public class CreateAsynchronousJobRunCommand : CreateJobRunCommand, IRequest
    {
        public CreateAsynchronousJobRunCommand()
        {
        }

        public CreateAsynchronousJobRunCommand(CreateJobRunCommand baseCommand, string callbackUrl)
            : base(baseCommand.ModuleId, baseCommand.MainModuleAssemblyName, baseCommand.MainModuleClassName, baseCommand.InputFiles, baseCommand.PointsNumber, baseCommand.RawArgumentsDictionary)
        {
            CallbackUri = callbackUrl;
        }

        public string CallbackUri { get; set; }
    }
}