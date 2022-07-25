using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace MS.Services.TaskCatalog.Api.Hubs
{
    [EnableCors]
    public class ChatHub : Hub
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
