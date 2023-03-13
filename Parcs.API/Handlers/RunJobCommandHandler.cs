﻿using MediatR;
using Parcs.Core;
using Parcs.HostAPI.Models.Commands;
using Parcs.HostAPI.Models.Responses;
using Parcs.HostAPI.Services.Interfaces;

namespace Parcs.HostAPI.Handlers
{
    public class RunJobCommandHandler : IRequestHandler<RunJobCommand, RunJobCommandResponse>
    {
        private readonly IHostInfoFactory _hostInfoFactory;
        private readonly IInputReaderFactory _inputReaderFactory;
        private readonly IMainModule _mainModule;
        private readonly IJobManager _jobManager;
        private readonly IDaemonSelector _daemonSelector;
        private readonly IInputWriter _inputWriter;

        public RunJobCommandHandler(
            IHostInfoFactory hostInfoFactory,
            IInputReaderFactory inputReaderFactory,
            IMainModule mainModule,
            IJobManager jobManager,
            IDaemonSelector daemonSelector,
            IInputWriter inputWriter)
        {
            _hostInfoFactory = hostInfoFactory;
            _inputReaderFactory = inputReaderFactory;
            _mainModule = mainModule;
            _jobManager = jobManager;
            _daemonSelector = daemonSelector;
            _inputWriter = inputWriter;
        }

        public async Task<RunJobCommandResponse> Handle(RunJobCommand request, CancellationToken cancellationToken)
        {
            var job = _jobManager.Create();

            await _inputWriter.WriteAllAsync(request.InputFiles, job.Id, job.CancellationToken);

            var selectedDaemons = _daemonSelector.Select(request.Daemons);
            job.SetDaemons(selectedDaemons);

            var hostInfo = _hostInfoFactory.Create(selectedDaemons);
            var inputReader = _inputReaderFactory.Create(job.Id);

            try
            {
                job.Start();
                var moduleOutput = await _mainModule.RunAsync(hostInfo, inputReader, job.CancellationToken);
                job.Finish(moduleOutput.Result);
            }
            catch (Exception ex)
            {
                job.Fail(ex.Message);
            }

            return new RunJobCommandResponse
            {
                ElapsedSeconds = job.ExecutionTime?.TotalSeconds,
                JobStatus = job.Status,
                ErrorMessage = job.ErrorMessage,
                Result = job.Result,
            };
        }
    }
}