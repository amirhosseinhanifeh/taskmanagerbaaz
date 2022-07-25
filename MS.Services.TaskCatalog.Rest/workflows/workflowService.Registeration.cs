using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Polly;
using RestEase;
using System.Net;

namespace MS.Services.TaskCatalog.Rest.Workflows
{
    public static class Registration
    {
        public static void AddWorkflowsApi(this IServiceCollection services, string path)
        {
            var policy = Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.NotFound)
                .RetryAsync();
            var p = new PolicyHttpMessageHandler(policy);

            var WorkflowApiV1 = RestClient.For<IWorkflowApiClientService>(path, p);
            services.AddSingleton(WorkflowApiV1);
        }

        public static void AddWorkflowsApi(this IServiceCollection services, string path, PolicyHttpMessageHandler policyHttpMessageHandler)
        {
            var WorkflowApiV1 = RestClient.For<IWorkflowApiClientService>(path, policyHttpMessageHandler);
            services.AddSingleton(WorkflowApiV1);
        }
    }
}
