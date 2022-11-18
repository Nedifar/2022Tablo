using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TabloBlazorMain.Server.PhoneTask
{
    public class MyHostedService : BackgroundService
    {
        private readonly IHubContext<GoodHubGbl> _hubContext;
        private IMemoryCache cache;
        public MyHostedService(IHubContext<GoodHubGbl> hubContext, IMemoryCache memoryCache)
        {
            _hubContext = hubContext;
            cache = memoryCache;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Выполняем задачу пока не будет запрошена остановка приложения
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await AsyncGetMethods.GetWeater(cache);
                    await _hubContext.Clients.All.SendAsync("SendWeather", cache.Get("weather"));
                }
                catch (Exception ex)
                {
                    if (!File.Exists("log.txt"))
                    {
                        File.Create("log.txt");
                    }
                    using (var stream = new StreamWriter("log.txt", true))
                    {
                        stream.WriteLine(DateTime.Now + " ----- " + "WeatherHostedError" + ex.Message);
                        stream.Close();
                    }
                }
                await Task.Delay(1000*15*60);
            }

            // Если нужно дождаться завершения очистки, но контролировать время, то стоит предусмотреть в контракте использование CancellationToken
        }
    }
}
