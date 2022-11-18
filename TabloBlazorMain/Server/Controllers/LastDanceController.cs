using ClosedXML.Excel;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TabloBlazorMain.Shared.LastDanceModels;
using TabloBlazorMain.Server.LastDanceResources;
using System.Text.RegularExpressions;
using TabloBlazorMain.Shared.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

namespace TabloBlazorMain.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LastDanceController : ControllerBase
    {
        IMemoryCache cache;
        DateTime selectedDate;
        Context.context context;
        private readonly IHubContext<GoodHubGbl> _hubContext;
        public LastDanceController(IHubContext<GoodHubGbl> hubContext, Context.context _context, IMemoryCache cache)
        {
            _hubContext = hubContext;
            context = _context;
            this.cache = cache;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
        }

        [HttpGet("getteacher/{teacher}")] //вернуть расписание по преподавателям
        public ActionResult GetTeach(string teacher)
        {
            IXLWorksheet result = null;
            result = (IXLWorksheet)cache.Get("xLMain");
            List<FullDayWeekClass> fullDayWeekClasses = new List<FullDayWeekClass>();
            //days.Add(new DayWeekClass { Day = "ЧКР" }); ??????????!!!!!!!
            for (int i = 1; i <= 6; i++)
            {
                int row = 6;
                List<DayWeekClass> teachers = Differents.raspisanieteach(row * i, teacher, result);
                string day = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(new DateTime(2022, 10, 16).AddDays(i).DayOfWeek);
                fullDayWeekClasses.Add(new FullDayWeekClass
                {
                    dayWeekName = day.ToUpper()[0] + day.Substring(1),
                    dayWeekClasses = teachers
                });
            }
            return Ok(fullDayWeekClasses);
        }

        [HttpGet("getgroup/{group}")]
        public ActionResult Get(string group) //вернуть расписание по группам
        {
            IXLWorksheet result = null;

            List<FullDayWeekClass> fullDayWeekClasses = new List<FullDayWeekClass>();
            result = (IXLWorksheet)cache.Get("xLMain");
            //days.Add(new DayWeekClass { Day = "ЧКР" }); ??????!!!!
            int column = Differents.IndexGroup(group, result);
            for (int j = 1; j <= 6; j++)
            {
                List<DayWeekClass> metrics = Differents.EnumerableMetrics(j * 6, column, result);
                string day = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(new DateTime(2022, 10, 16).AddDays(j).DayOfWeek);
                fullDayWeekClasses.Add(new FullDayWeekClass
                {
                    dayWeekName = day.ToUpper()[0] + day.Substring(1),
                    dayWeekClasses = metrics
                });
            }
            return Ok(fullDayWeekClasses);
        }

        [HttpGet("getcabinet/{cabinet}")] //вернуть расписание по кабинетам
        public ActionResult GetKab(string cabinet)
        {
            IXLWorksheet result = null;
            List<FullDayWeekClass> fullDayWeekClasses = new List<FullDayWeekClass>();
            result = (IXLWorksheet)cache.Get("xLMain");
            //days.Add(new DayWeekClass { Day = "ЧКР" }); ?????!!!!
            for (int i = 1; i <= 6; i++)
            {
                int row = 6;
                List<DayWeekClass> сabinets = Differents.raspisaniekab(row * i, cabinet, result);
                string day = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(new DateTime(2022, 10, 16).AddDays(i).DayOfWeek);
                fullDayWeekClasses.Add(new FullDayWeekClass
                {
                    dayWeekName = day.ToUpper()[0] + day.Substring(1),
                    dayWeekClasses = сabinets

                });
            }
            return Ok(fullDayWeekClasses);
        }

        [HttpGet]
        [Route("getcabinetsList")]
        public async Task<ActionResult<IEnumerable<List<string>>>> Get() //вернуть список кабиентов
        {
            while ((List<string>)cache.Get("MainListCabinets") is null)
            {

            }
            return Ok((List<string>)cache.Get("MainListCabinets"));
        }
        [HttpGet]
        [Route("getteachersList")]
        public async Task<ActionResult<IEnumerable<List<string>>>> GetTeachList() //вернуть список преподавателей
        {
            while ((List<string>)cache.Get("MainListTeachers") is null)
            {

            }
            return Ok((List<string>)cache.Get("MainListTeachers"));
        }

        [HttpGet]
        [Route("getgroupList")]
        public async Task<ActionResult<IEnumerable<List<string>>>> get() //вернуть список групп
        {
            while ((List<string>)cache.Get("MainListGroups") is null)
            {

            }
            return Ok((List<string>)cache.Get("MainListGroups"));
        }

        public static void RaspisanieIzm(XLWorkbook _workbook1, int h, IXLWorksheet ix) //Метод для перезаписи в расписании изменений
        {
            DateTime dateIZM = DateTime.Today;
            int dayWeek = (int)DateTime.Today.DayOfWeek;
            var worksheet = _workbook1.Worksheets.First();
            for (int i = 1; i <= worksheet.ColumnsUsed().Count(); i++)
            {
                int n = worksheet.RowsUsed().Count();
                for (int j = 11; j <= worksheet.RowsUsed().Count() + 10; j++)
                {
                    for (int l = 3; l <= ix.ColumnsUsed().Count(); l++)
                    {
                        if (ix.Cell(5, l).GetValue<string>() == worksheet.Cell(j, i).GetValue<string>())
                        {
                            bool a = false;
                            int g = 6;
                            for (int m = 1; m <= g; m++)
                            {
                                IXLCell leg = worksheet.Cell(j + m, i);
                                if (leg.Style.Font.FontSize >= 22 || leg.Value.ToString() == "" || a || leg.Value.ToString().Length == 4)
                                {
                                    if (ix.Cell(27, 2).Value.ToString() != "4")
                                        ix.Cell((6 * h) + m, l).Value = " ";
                                    else
                                        ix.Cell((6 * h) + m - 1, l).Value = " ";
                                    a = true;
                                }
                                else
                                {
                                    if (ix.Cell(27, 2).Value.ToString() != "4")
                                        ix.Cell((6 * h) + m, l).Value = worksheet.Cell(j + m, i);
                                    else
                                        ix.Cell((6 * h) + m - 1, l).Value = worksheet.Cell(j + m, i);
                                }
                            }
                        }
                    }
                }
            }
        }

        [HttpPost]
        [Route("getFloorShedule")]
        public async Task<ActionResult> GetFlooreShedule(Shared.LastDanceModels.PostFloorModel models)
        {
            try
            {
                List<DayWeekClass> weekClasses = new List<DayWeekClass>();
                while ((List<string>)cache.Get("MainListCabinets") is null)
                {

                }
                while ((IXLWorksheet)cache.Get("xLMain") is null)
                {

                }
                while ((List<FloorCabinet>)cache.Get("FullFloorShedule") is null)
                {

                }
                var iX = (IXLWorksheet)cache.Get("xLMain");
                List<string> cabinets = (List<string>)cache.Get("MainListCabinets");
                List<FloorCabinet> floorsCabinets = (List<FloorCabinet>)cache.Get("FullFloorShedule");
                List<string> filtredCabinets = new List<string>();
                Regex regex;
                switch (models.floor)
                {
                    case "11":
                        regex = new Regex("^1[0-9]{2}");
                        break;
                    case "12":
                        regex = new Regex("^1[0-9]{1}[^0-9]{0,1}$");
                        break;
                    case "21":
                        regex = new Regex("^2[0-9]{2}");
                        break;
                    case "22":
                        regex = new Regex("^2[0-9]{1}[^0-9]{0,1}$");
                        break;
                    case "31":
                        regex = new Regex("^3[0-9]{2}");
                        break;
                    case "32":
                        regex = new Regex("^3[0-9]{1}[^0-9]{0,1}$");
                        break;
                    case "41":
                        regex = new Regex("^4[0-9]{2}");
                        break;
                    case "42":
                        regex = new Regex("^4[0-9]{1}[^0-9]{0,1}$");
                        break;
                    default:
                        regex = new Regex("[0-9]{2,3}");
                        break;
                }
                if (int.TryParse(models.paraNow, out int numberPara) || models.paraNow == "ЧКР")
                {
                    try
                    {
                        foreach (string cabinet in cabinets)
                        {
                            if (regex.IsMatch(cabinet))
                            {
                                var psevdores = floorsCabinets.Where(p => p.Name == cabinet).FirstOrDefault();
                                if (models.CHKR.Where(p => p.TypeInterval.name == "ЧКР").FirstOrDefault() != null)
                                {
                                    var item = new Shared.LastDanceModels.DayWeekClass { Day = "ЧКР" };
                                    var usl = models.CHKR.Where(p => p.TypeInterval.name == "ЧКР" || p.TypeInterval.name == "Пара").ToList();
                                    for (int i = 0; i < usl.Count(); i++)
                                    {
                                        if (usl[i].TypeInterval.name == "ЧКР")
                                        {
                                            if (i == 0)
                                            {
                                                item.cabinet = cabinet;
                                                item.pp = "Сейчас идёт ЧКР";
                                            }
                                            else
                                            {
                                                item.pp = weekClasses[i - 1].pp;
                                                psevdores.DayWeeks[i - 1].pp = "Сейчас будет ЧКР\n" + psevdores.DayWeeks[i - 1].pp;
                                            }
                                            psevdores.DayWeeks.Insert(i, item);
                                            break;
                                        }
                                    }
                                }
                                if (models.paraNow == "ЧКР") { weekClasses.Add(psevdores.DayWeeks.Where(p => p.Number == null).FirstOrDefault()); }
                                else { weekClasses.Add(psevdores.DayWeeks.Where(p => p.Number == numberPara).FirstOrDefault()); }
                            }
                        }
                    }
                    catch (Exception ex) { }
                }


                else if (models.paraNow is null)
                {
                    foreach (string cabinet in cabinets)
                    {
                        if (regex.IsMatch(cabinet))
                        {
                            weekClasses.Add(new DayWeekClass { cabinet = cabinet });
                        }
                    }
                }

                return Ok(weekClasses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCabinentsWithDetail")]
        public async Task<ActionResult> GetCabinentsWithDetail(string cabinet)
        {
            List<DayWeekClass> weekClasses = new List<DayWeekClass>();
            while ((List<string>)cache.Get("MainListCabinets") is null)
            {

            }
            while ((IXLWorksheet)cache.Get("xLMain") is null)
            {

            }
            while ((List<FloorCabinet>)cache.Get("FullFloorShedule") is null)
            {

            }
            var iX = (IXLWorksheet)cache.Get("xLMain");
            List<FloorCabinet> floorsCabinets = (List<FloorCabinet>)cache.Get("FullFloorShedule");
            var resultWeekClass = new List<DayWeekClass>();
            resultWeekClass = floorsCabinets.Where(p => p.Name == cabinet).FirstOrDefault()
                ?.DayWeeks.ToList();
            return Ok(resultWeekClass);
        }

        [HttpGet("update")] //запрос для обновления данных между сервером и объявлениями
        public async Task<ActionResult> PostUpdateChangesAnnouncment()
        {
            var list = context.Announcements.ToList();
            var linka = new List<Announcement>();
            int i = 1;
            foreach (var lili in list.Where(p => p.dateAdded < DateTime.Now && (p.dateClosed >= DateTime.Now || p.dateClosed == null) && p.isActive).OrderByDescending(p => p.Priority).ThenByDescending(p => p.idAnnouncement).Take(5))
            {
                linka.Add(lili);
                linka.LastOrDefault().idAnnouncement = i;
                i++;
            }
            cache.Set("ann", linka, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15) });
            using (var stream = new StreamWriter("log.txt", true))
            {
                stream.WriteLine(DateTime.Now + " ----- " + $"SendAnn");
                stream.Close();
            }
            await _hubContext.Clients.All.SendAsync("SendAnn", linka);
            return Ok();
        }
    }
}
