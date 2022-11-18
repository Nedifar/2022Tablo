using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TabloBlazorMain.Server.LastDanceResources;
using TabloBlazorMain.Shared.Models;

namespace TabloBlazorMain.Server.PhoneTask
{
    public class AsyncGetMethods
    {
        public static async Task GetWeater(IMemoryCache cache)
        {
            using (var http = new HttpClient())
            {
                var response = await http.GetAsync("https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/Orenburg?unitGroup=metric&include=current&key=WBQY6NYB9YGMHSZHQYVEHLWBJ&contentType=json");
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                var jsonParse = JObject.Parse(result);
                string s = (string)jsonParse["currentConditions"]["temp"];
                string actualWeather = Convert.ToInt32(s).ToString() + "°C";
                cache.Set("weather", actualWeather, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15) });
            }
        }
        public static async Task GetUpdate(Context.context context)
        {
            RequestForDynamicUpdate request = new RequestForDynamicUpdate();
            request.timeNow = DateTime.Now.ToString("HH:mm");
            request.labelPara = "Сейчас идет";
            try
            {
                var list = context.TimeShedules.ToList();
                TimeShedule relevantTimeShedule;
                if (list.Where(p => p.Name == DateTime.Now.ToShortDateString()).Count() != 0)
                {
                    FormingMainUpdate(list.Where(p => p.Name == DateTime.Now.ToShortDateString()).FirstOrDefault(), request);
                }
                else if ((int)DateTime.Now.DayOfWeek == context.SpecialDayWeekNames.FirstOrDefault().dayWeek)
                {
                    FormingMainUpdate(relevantTimeShedule = list.Where(p => p.Name == "ЧКР").FirstOrDefault(), request);
                }
                else if ((int)DateTime.Now.DayOfWeek == 0)
                {
                    request.labelPara = "";
                    request.tbNumberPara = "Сегодня, по сути, воскресенье, надо отдыхать.";
                    GlobalObjects.request = request;
                    return;
                }
                else
                {
                    FormingMainUpdate(list.Where(p => p.Name == "Основной").FirstOrDefault(), request);
                }
            }
            catch { GlobalObjects.request = request; }
        }

        private static void FormingMainUpdate(TimeShedule relevantTimeShedule, RequestForDynamicUpdate request)
        {
            try
            {
                request.paraNow = null;
                request.paraNow = relevantTimeShedule.onlyParaNow()?.outGraphicNewTablo;
            }
            catch { request.paraNow = null; }
            var rrList = relevantTimeShedule.Paras.OrderBy(p => p.numberInList).ToList();
            request.lv = rrList;
            request.grLineHeight = relevantTimeShedule.TotalTime();
            Main.height = request.grLineHeight;
            double height = (DateTime.Now.TimeOfDay.TotalMinutes - rrList[0].begin.TimeOfDay.TotalMinutes);
            if (height < 0)
            {
                request.lineMargin = 0;
                request.colorLineHeight = 0;
                if ((rrList[0].begin.TimeOfDay - DateTime.Now.TimeOfDay).TotalMinutes <= 20)
                {
                    request.tbNumberPara = $"До начала пар {Math.Ceiling((rrList[0].begin.TimeOfDay - DateTime.Now.TimeOfDay).TotalMinutes)} мин.";
                }
                else
                    request.tbNumberPara = "На сегодня пары еще не начались.";
                request.labelPara = "";
                GlobalObjects.request = request;
                return;
            }
            request.lineMarginTop = height;
            request.colorLineHeight = height;
            if (rrList.LastOrDefault().end.TimeOfDay < DateTime.Now.TimeOfDay)
            {
                request.lineMarginTop = request.grLineHeight - 1;
                request.tbNumberPara = "На сегодня занятия закончились.";
                request.labelPara = "";
                GlobalObjects.request = request;
                return;
            }
            request.progressBarPara = relevantTimeShedule.paraNow().toEndTimeInProcent;
            request.toEndPara = relevantTimeShedule.paraNow().toEndTime;
            request.tbNumberPara = relevantTimeShedule.paraNow().labelPara;
            Main.fulltime = relevantTimeShedule.times();
            GlobalObjects.request = request;
            return;
        }
    }
}
