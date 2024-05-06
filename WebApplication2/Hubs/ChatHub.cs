using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication2.Hubs
{
    [Authorize]
    public class ChatHub:Hub
    {

        public async Task SendMessage(string user, string message)
        {

            var name = Context.User.Identity.Name;
            Console.WriteLine(Context.ConnectionId);
            await Clients.All.SendAsync("ReceiveMessage", name??user, message);
        }
    }

    
}
