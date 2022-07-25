using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Polly;
using RestEase;
using System.Net;

namespace MS.Services.TaskCatalog.Rest.Tasks
{
    public static class Registration
    {
        public static void AddTasksApi(this IServiceCollection services, string path)
        {
            var policy = Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.NotFound)
                .RetryAsync();
            var p = new PolicyHttpMessageHandler(policy);

            var TaskApiV1 = RestClient.For<ITaskApiClientService>(path, p);
            services.AddSingleton(TaskApiV1);
        }

        public static void AddTasksApi(this IServiceCollection services, string path, PolicyHttpMessageHandler policyHttpMessageHandler)
        {
            var TaskApiV1 = RestClient.For<ITaskApiClientService>(path, policyHttpMessageHandler);
            services.AddSingleton(TaskApiV1);
        }
    }
}
