using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TabloBlazorMain.Server.PhoneTask
{
    public class WeekNameHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHubContext<GoodHubGbl> _hubContext;
        private IMemoryCache cache;
        private Context.context context;
        public WeekNameHostedService(IHubContext<GoodHubGbl> hubContext, IMemoryCache memoryCache, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _hubContext = hubContext;
            cache = memoryCache;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var scope = _serviceScopeFactory.CreateScope();
                    var _terminalService = scope.ServiceProvider.GetRequiredService<Context.context>();
                    context = _terminalService; string result = String.Empty;
                    var dt = context.WeekNames.ToList().LastOrDefault();
                    var cal = new GregorianCalendar();
                    var weekNumber = cal.GetWeekOfYear(dt.Begin, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
                    var selectedWeekNumber = cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
                    if ((weekNumber % 2 == 0 && selectedWeekNumber % 2 == 0) || (weekNumber % 2 != 0 && selectedWeekNumber % 2 != 0))
                    {
                        if (dt.Name.ToLower().Contains("верх"))
                            result = "Верхняя неделя";
                        else
                            result = "Нижняя неделя";
                    }
                    else
                    {
                        if (dt.Name.ToLower().Contains("верх"))
                            result = "Нижняя неделя";
                        else
                            result = "Верхняя неделя";
                    }
                    cache.Set("weekName", result, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) });
                    await _hubContext.Clients.All.SendAsync("SendWeekName", cache.Get("weekName"));
                }
                catch (Exception ex)
                {
                    if (!File.Exists("log.txt"))
                    {
                        File.Create("log.txt");
                    }
                    using (var stream = new StreamWriter("log.txt", true))
                    {
                        stream.WriteLine(DateTime.Now + " ----- " + "WeekNameHostedError" + ex.Message);
                        stream.Close();
                    }
                }
                await Task.Delay(1000 * 60 * 60);
            }
        }
    }
}
