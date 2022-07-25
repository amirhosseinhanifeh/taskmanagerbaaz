using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using MS.Services.TaskCatalog.Rest.Tasks;
using Polly;
using RestEase;
using System.Net;

namespace MS.Services.TaskCatalog.Rest.Users
{
    public static class Registration
    {
        public static void AddUsersApi(this IServiceCollection services, string path)
        {
            var policy = Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.NotFound)
                .RetryAsync();
            var p = new PolicyHttpMessageHandler(policy);

            var TaskApiV1 = RestClient.For<IUserApiClientService>(path, p);
            services.AddSingleton(TaskApiV1);
        }

        public static void AddUsersApi(this IServiceCollection services, string path, PolicyHttpMessageHandler policyHttpMessageHandler)
        {
            var TaskApiV1 = RestClient.For<IUserApiClientService>(path, policyHttpMessageHandler);
            services.AddSingleton(TaskApiV1);
        }
    }
}
