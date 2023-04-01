﻿using Parcs.Net;
using System.Text;

namespace Parcs.Shared.Models
{
    public sealed class Job
    {
        private bool _hasBeenRun;
        private bool _canBeCancelled;
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private readonly List<Daemon> _executedOnDaemons = new();

        public Job(Guid moduleId, string assemblyName, string className)
        {
            Id = Guid.NewGuid();
            CreateDateUtc = DateTime.UtcNow;
            Status = JobStatus.New;
            ModuleId = moduleId;
            AssemblyName = assemblyName;
            ClassName = className;
            _hasBeenRun = false;
            _canBeCancelled = true;
        }

        public Guid Id { get; private set; }

        public Guid ModuleId { get; private set; }

        public string ModuleName { get; private set; }

        public string AssemblyName { get; private set; }

        public string ClassName { get; private set; }

        public IMainModule MainModule { get; private set; }

        public JobStatus Status { get; private set; }

        public DateTime CreateDateUtc { get; private set; }

        public DateTime? StartDateUtc { get; private set; }

        public DateTime? EndDateUtc { get; private set; }

        public string ErrorMessage { get; private set; }

        public IReadOnlyCollection<Daemon> ExecutedOnDaemons => _executedOnDaemons.AsReadOnly();

        public TimeSpan? ExecutionTime => EndDateUtc is null ? default : EndDateUtc - StartDateUtc;

        public CancellationToken CancellationToken => _cancellationTokenSource.Token;

        public void Start()
        {
            if (_hasBeenRun)
            {
                throw new ArgumentException($"The job can't be run anymore. Status: {Status}");
            }

            StartDateUtc = DateTime.UtcNow;
            Status = JobStatus.InProgress;

            _hasBeenRun = true;
        }

        public void Finish()
        {
            Status = JobStatus.Completed;
            OnFinished();
        }

        public void Fail(string errorMessage)
        {
            if (Status == JobStatus.Cancelled)
            {
                return;
            }

            Status = JobStatus.Error;
            ErrorMessage = errorMessage;

            OnFinished();
        }

        public void Cancel()
        {
            if (!_canBeCancelled)
            {
                return;
            }

            Status = JobStatus.Cancelled;
            _cancellationTokenSource.Cancel();

            OnFinished();
        }

        public void TrackExecution(Daemon daemon)
        {
            _executedOnDaemons.Add(daemon);
        }

        public void SetMainModule(IMainModule mainModule)
        {
            MainModule = mainModule;
            ModuleName = mainModule.Name;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Job Id: {Id}")
                .AppendLine($"Module Id: {ModuleId}")
                .AppendLine($"Main module name: {ModuleName}")
                .AppendLine($"Main module assembly: {AssemblyName}")
                .AppendLine($"Main module class: {ClassName}")
                .AppendLine($"Status: {Status}")
                .AppendLine($"Created: {CreateDateUtc}")
                .AppendLine($"Started: {StartDateUtc}")
                .AppendLine($"Finished: {EndDateUtc}")
                .AppendLine($"Execution time: {ExecutionTime}")
                .AppendLine($"Execution nodes number: {ExecutedOnDaemons.Count}")
                .AppendLine(ErrorMessage is null ? null : $"Error message: {ErrorMessage}")
                .ToString();
        }

        private void OnFinished()
        {
            _canBeCancelled = false;
            EndDateUtc = DateTime.UtcNow;
            MainModule = null;
        }
    }
}