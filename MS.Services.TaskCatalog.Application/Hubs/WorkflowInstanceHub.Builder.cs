using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;

namespace MS.Services.TaskCatalog.Application.Hubs
{

    public class WorkflowInstanceHub
    {
        public static HubConnection connection;
        //private static string Url = "https://localhost:7153";
        private static string Url = "http://192.168.10.177";
        public static async Task Connect()
        {
            if (connection == null)
            {
                connection = new HubConnectionBuilder()
                              .WithUrl(Url + "/workflowhub")
                              .Build();

                connection.Closed += async (error) =>
                {
                    await Task.Delay(new Random().Next(0, 5) * 1000);
                    await connection.StartAsync();
                };
                try
                {
                    await connection.StartAsync();
                }
                catch (Exception ex)
                {
                }
            }
        }
        public static async Task UpdateWorkflowInstanceById(object model)
        {
           await Connect();
            await connection.InvokeAsync("SendMessage", model);
        }
    }
}
