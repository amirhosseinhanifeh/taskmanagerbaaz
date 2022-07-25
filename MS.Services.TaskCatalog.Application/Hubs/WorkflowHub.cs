using Microsoft.AspNetCore.SignalR;

namespace MS.Services.TaskCatalog.Application.Hubs
{
    public class WorkflowHub:Hub
    {
        public async Task SendMessage(object model)
        {
            await Clients.All.SendAsync("GetWorkflow", model);
        }
        public override Task OnConnectedAsync()
        {

            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
