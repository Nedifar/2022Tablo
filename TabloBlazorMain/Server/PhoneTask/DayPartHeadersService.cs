using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TabloBlazorMain.Server.PhoneTask
{
    public class DayPartHeadersService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHubContext<GoodHubGbl> _hubContext;
        private IMemoryCache cache;
        private Context.context context;
        public DayPartHeadersService(IHubContext<GoodHubGbl> hubContext, IMemoryCache memoryCache, IServiceScopeFactory serviceScopeFactory)
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
                    string result = context.DayPartHeaders.ToList().FirstOrDefault(p => p.beginTime.TimeOfDay <= DateTime.Now.TimeOfDay && p.endTime.TimeOfDay >= DateTime.Now.TimeOfDay)?.Header;
                    if (result != null)
                    {
                        cache.Set("dayPart", result, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) });
                    }
                    else
                    {
                        cache.Set("dayPart", "no", new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) });
                    }
                    await _hubContext.Clients.All.SendAsync("SendDatPart", cache.Get("dayPart"));
                }
                catch (Exception ex)
                {
                }
                await Task.Delay(1000 * 60 * 5);
            }

            // Если нужно дождаться завершения очистки, но контролировать время, то стоит предусмотреть в контракте использование CancellationToken
        }
    }
}
