﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Parcs.Portal.Constants;
using Parcs.Portal.Models;
using Parcs.Portal.Models.Host;
using Parcs.Portal.Models.Host.Requests;
using Parcs.Portal.Models.Host.Responses;
using Parcs.Portal.Services.Interfaces;

namespace Parcs.Portal.Components
{
    public class NewJobBase : PageBase
    {
        [Inject]
        protected IHostClient HostClient { get; set; }

        protected Dictionary<string, List<string>> HostErrors { get; set; } = new();

        [Parameter]
        public long ModuleId { get; set; } 

        protected GetModuleHostResponse Module { get; set; }

        protected List<string> CurrentAssemblyImplementations { get; set; } = new();

        protected CreateJobViewModel CreateJobViewModel { get; set; } = new ();

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;

            Module = await HostClient.GetModuleAsync(ModuleId, cancellationTokenSource.Token);

            IsLoading = false;
        }

        protected async Task CreateJobAsync()
        {
            IsLoading = true;

            var createJobRequest = new CreateJobHostRequest
            {
                ClassName = CreateJobViewModel.ClassName,
                AssemblyName = CreateJobViewModel.AssemblyName,
                InputFiles = CreateJobViewModel.InputFiles ?? Enumerable.Empty<IBrowserFile>(),
                ModuleId = ModuleId,
            };

            try
            {
                await HostClient.PostJobAsync(createJobRequest, cancellationTokenSource.Token);
            }
            catch (HostException ex)
            {
                HostErrors = ex.ProblemDetails.Errors;
            }
            catch
            {
                HostErrors = new Dictionary<string, List<string>>()
                {
                    { "Error", new List<string> { "An error occurred while communicating with the Host." } }
                };
            }

            HostErrors.Clear();

            IsLoading = false;

            await JsRuntime.InvokeVoidAsync(JSExtensionMethods.BackToPreviousPage);
        }

        protected void OnFileChanged(InputFileChangeEventArgs e)
        {
            CreateJobViewModel.InputFiles = e.GetMultipleFiles();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JsRuntime.InvokeVoidAsync(JSExtensionMethods.SetSelect2);

            var dotNetReference = DotNetObjectReference.Create(this);

            await JsRuntime.InvokeVoidAsync(
                JSExtensionMethods.SetOnChangeSelect2, "select-class", dotNetReference, JSInvokableMethods.ChangeClass);

            await JsRuntime.InvokeVoidAsync(
                JSExtensionMethods.SetOnChangeSelect2, "select-assembly", dotNetReference, JSInvokableMethods.ChangeAssembly);
        }
    }
}