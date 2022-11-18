using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TabloBlazorMain.Server.PhoneTask
{
    public class AdminHosted : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHubContext<GoodHubGbl> _hubContext;
        private IMemoryCache cache;
        private Context.context context;
        public AdminHosted(IHubContext<GoodHubGbl> hubContext, IMemoryCache memoryCache, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
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
                    var scope = _serviceScopeFactory.CreateScope();
                    var _terminalService = scope.ServiceProvider.GetRequiredService<Context.context>();
                    context = _terminalService;
                    var ll = context.MonthYear.ToList();
                    string result = context.MonthYear.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault().getSupervisorNow;
                    cache.Set("admin", result, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15) });
                    await _hubContext.Clients.All.SendAsync("SendAdmin", cache.Get("admin"));
                }
                catch (Exception ex)
                {
                    if (!File.Exists("log.txt"))
                    {
                        File.Create("log.txt");
                    }
                    using (var stream = new StreamWriter("log.txt", true))
                    {
                        stream.WriteLine(DateTime.Now + " ----- " + "AdminHostedError" + ex.Message);
                        stream.Close();
                    }
                }
                await Task.Delay(1000 * 15 * 60);
            }

            // Если нужно дождаться завершения очистки, но контролировать время, то стоит предусмотреть в контракте использование CancellationToken
        }
    }
}
