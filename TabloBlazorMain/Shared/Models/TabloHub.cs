using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloBlazorMain.Shared.Models
{
    public class TabloHub : Hub
    {
        public static HubConnection hub {get; set;}
        public async Task Send(string username, string message)
        {
            //await Clients.All.SendAsync("Send", message);
            await hub.SendAsync("Send", message);
        }
        public override Task OnConnectedAsync()
        {
            Debug.WriteLine(Context.ConnectionId);
            return base.OnConnectedAsync();
        }
    }
}
