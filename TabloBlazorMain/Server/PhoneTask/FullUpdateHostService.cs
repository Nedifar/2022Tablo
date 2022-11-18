using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TabloBlazorMain.Server.LastDanceResources;

namespace TabloBlazorMain.Server.PhoneTask
{
    public class FullUpdateHostService : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IHubContext<GoodHubGbl> _hubContext;
        private Context.context context;
        private IMemoryCache cache;
        public FullUpdateHostService(IHubContext<GoodHubGbl> hubContext, IMemoryCache memoryCache, IServiceScopeFactory _serviceScopeFactory)
        {
            serviceScopeFactory = _serviceScopeFactory;

            _hubContext = hubContext;
            cache = memoryCache;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Выполняем задачу пока не будет запрошена остановка приложения
            while (!stoppingToken.IsCancellationRequested)
            {
                var scope = serviceScopeFactory.CreateScope();
                var contextService = scope.ServiceProvider.GetRequiredService<Context.context>();
                context = contextService;
                try
                {
                    await AsyncGetMethods.GetUpdate(context);
                    await _hubContext.Clients.All.SendAsync("SendRequest", GlobalObjects.request);
                }
                catch (Exception ex)
                {
                    if (!File.Exists("log.txt"))
                    {
                        File.Create("log.txt");
                    }
                    using (var stream = new StreamWriter("log.txt", true))
                    {
                        stream.WriteLine(DateTime.Now + " ----- " + "FullUpdHostedError" + ex.Message);
                        stream.Close();
                    }
                }
                await Task.Delay(5000);
            }

            // Если нужно дождаться завершения очистки, но контролировать время, то стоит предусмотреть в контракте использование CancellationToken
        }
    }
}
