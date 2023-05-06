﻿using MediatR;
using Parcs.HostAPI.Models.Commands;
using Parcs.HostAPI.Models.Responses;
using Parcs.HostAPI.Services.Interfaces;
using Parcs.Core.Models.Enums;
using Parcs.Core.Services.Interfaces;

namespace Parcs.HostAPI.Handlers
{
    public sealed class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, CreateJobCommandResponse>
    {
        private readonly IJobManager _jobManager;
        private readonly IJobDirectoryPathBuilder _jobDirectoryPathBuilder;
        private readonly IFileSaver _fileSaver;

        public CreateJobCommandHandler(
            IJobManager jobManager,
            IJobDirectoryPathBuilder jobDirectoryPathBuilder,
            IFileSaver fileSaver)
        {
            _jobManager = jobManager;
            _jobDirectoryPathBuilder = jobDirectoryPathBuilder;
            _fileSaver = fileSaver;
        }

        public async Task<CreateJobCommandResponse> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var job = _jobManager.Create(request.ModuleId, request.AssemblyName, request.ClassName);

            var inputPath = _jobDirectoryPathBuilder.Build(job.Id, JobDirectoryGroup.Input);
            await _fileSaver.SaveAsync(request.InputFiles, inputPath, job.CancellationToken);

            return new CreateJobCommandResponse(job);
        }
    }
}