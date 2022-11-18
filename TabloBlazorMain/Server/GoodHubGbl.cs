using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TabloBlazorMain.Server
{
    public class GoodHubGbl : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine(Context.ConnectionId);
            if(!File.Exists("log.txt"))
            {
                File.Create("log.txt");
            }
                using (var stream = new StreamWriter("log.txt", true))
                {
                    stream.WriteLine(DateTime.Now + " ----- " + Context.ConnectionId);
                    stream.Close();
                }

                return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            using (var stream = new StreamWriter("log.txt", true))
            {
                stream.WriteLine(DateTime.Now + " ----- " + $"Disconnected {e?.Message} {Context.ConnectionId}");
                stream.Close();
            }
            await base.OnDisconnectedAsync(e);
        }
    }
}
