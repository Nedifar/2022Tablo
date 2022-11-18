using ClosedXML.Excel;
using HtmlAgilityPack;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TabloBlazorMain.Shared.LastDanceModels;

namespace TabloBlazorMain.Server.LastDanceResources
{
    public class Differents
    {
        public static WebClient webClient = new WebClient();
        public static List<string> dataforcb = new List<string>();
        public static XLWorkbook _workbook;
        public int upDay = 0;
        public int downDay = 0;
        public DateTime DupDay;
        public DateTime DdownDay;
        public string downMonth;
        public string upMonth;
        public void DateOut(DateTime dt) //метод для определения начала и конца недели
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    CleanCache();
                    downDay = dt.AddDays(1).Day;
                    upDay = dt.AddDays(6).Day;
                    DupDay = dt.AddDays(6);
                    DdownDay = dt.AddDays(1);
                    break;
                case DayOfWeek.Monday:
                    downDay = dt.Day;
                    upDay = dt.AddDays(5).Day;
                    DupDay = dt.AddDays(5);
                    DdownDay = dt;
                    break;
                case DayOfWeek.Tuesday:
                    downDay = dt.AddDays(-1).Day;
                    upDay = dt.AddDays(4).Day;
                    DupDay = dt.AddDays(4);
                    DdownDay = dt.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    downDay = dt.AddDays(-2).Day;
                    upDay = dt.AddDays(3).Day;
                    DupDay = dt.AddDays(3);
                    DdownDay = dt.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    downDay = dt.AddDays(-3).Day;
                    upDay = dt.AddDays(2).Day;
                    DupDay = dt.AddDays(2);
                    DdownDay = dt.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    downDay = dt.AddDays(-4).Day;
                    upDay = dt.AddDays(1).Day;
                    DupDay = dt.AddDays(1);
                    DdownDay = dt.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    downDay = dt.AddDays(-5).Day;
                    upDay = dt.Day;
                    DupDay = dt;
                    DdownDay = dt.AddDays(-5);
                    break;
            }
            downMonth = Mouth(DdownDay);
            upMonth = Mouth(DupDay);
        }

        public static void CleanCache() //метод для удаления устаревших скачанных изменений
        {
            //CultureInfo culture = new CultureInfo("ru-RU");
            //for (int i = 0; i < 6; i++)
            //{
            //    try
            //    {
            //        File.Delete(@$"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie\{DdownDay.AddDays(i - 7).ToString("d", culture)}.xls");
            //        File.Delete(@$"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie\{DdownDay.AddDays(i - 7).ToString("d", culture)}.xlsx");
            //    }
            //    catch
            //    {
            //        continue;
            //    }
            //}
            //try
            //{
            //    string trim1 = Mouth(DupDay.AddDays(-7)).Substring(0, 3);
            //    File.Delete($"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie/_{Differents.downDay - 7}_{Differents.upDay - 7}_{trim1}.xlsx");
            //}
            //catch
            //{

            //}
        }

        public string Mouth(DateTime date) // метод для возвращения название месяца по дате 
        {
            var lines = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie/Month.txt");
            string[] massiv = lines.Split('\n');
            switch (date.Month)
            {
                case 9:
                    return massiv[8];
                case 10:
                    return massiv[9];
                case 11:
                    return massiv[10];
                case 12:
                    return massiv[11];
                case 1:
                    return massiv[0];
                case 2:
                    return massiv[1];
                case 3:
                    return massiv[2];
                case 4:
                    return massiv[3];
                case 5:
                    return massiv[4];
                case 6:
                    return massiv[5];
                case 7:
                    return massiv[6];
                case 8:
                    return massiv[7];
                default:
                    break;
            }
            return null;
        }

        public static void DownloadFeatures(DateTime time, IXLWorksheet xi) //Метод для скачивания изменений
        {
            string data = "";
            using (var stream = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie/allizm.txt"))
            {
                data = stream.ReadToEnd();
                stream.Close();
                DateTime dateIZM = time;
                int dayWeek = (int)time.DayOfWeek;
                WebClient web = new WebClient();
                CultureInfo culture = new CultureInfo("ru-RU");
                HtmlDocument doc = new HtmlDocument();
                var web1 = new HtmlWeb();
                doc = web1.Load("https://oksei.ru/studentu/raspisanie_uchebnykh_zanyatij");
                var nodes = doc.DocumentNode.SelectNodes("//*[@class='attachment a-xls']/p/a");
                for (int i = 1; i <= dayWeek + 1; i++)
                {
                    if (data.Contains($"{dateIZM.AddDays(i - dayWeek).ToString("d", culture)}.xlsx"))
                    {
                        continue;
                    }
                    else
                    {
                        var nodeForNeedDate = nodes.Where(p => p.InnerText.Contains(dateIZM.AddDays(i - dayWeek).ToString("d", culture))
                        || p.InnerText.Contains($"{dateIZM.AddDays(i - dayWeek).Day}.{dateIZM.AddDays(i - dayWeek).Month}.{dateIZM.AddDays(i - dayWeek).Year}")).FirstOrDefault();
                        if (nodeForNeedDate == null)
                        {
                            continue;
                        }
                        else
                        {
                            try
                            {
                                var href = nodeForNeedDate.Attributes["href"].Value;
                                web.DownloadFile(@$"https://oksei.ru{href}", @$"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie\{dateIZM.AddDays(i - dayWeek).ToString("d", culture)}.xls");
                                Workbook workbook2 = new Workbook();
                                string a = @$"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie\{dateIZM.AddDays(i - dayWeek).ToString("d", culture)}.xls";
                                workbook2.LoadFromFile(@$"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie\{dateIZM.AddDays(i - dayWeek).ToString("d", culture)}.xls");
                                workbook2.SaveToFile(@$"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie\{dateIZM.AddDays(i - dayWeek).ToString("d", culture)}.xlsx", ExcelVersion.Version2013);
                                XLWorkbook xL = new XLWorkbook(@$"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie\{dateIZM.AddDays(i - dayWeek).ToString("d", culture)}.xlsx");
                                Controllers.LastDanceController.RaspisanieIzm(xL, i, xi);

                                data += $"{dateIZM.AddDays(i - dayWeek).ToString("d", culture)}.xlsx\n";
                                using (StreamWriter streamWriter = new StreamWriter(@$"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie\allizm.txt", false, System.Text.Encoding.Default))
                                {
                                    streamWriter.Write(data);
                                    streamWriter.Close();
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
        }

        public static List<DayWeekClass> EnumerableMetrics(int row, int column, IXLWorksheet ix) //Метод для получения расписания из xlsx
        {
            int count = 1;
            List<DayWeekClass> dayWeeks = new List<DayWeekClass>();
            for (int i = row; i < row + 6; i++)
            {
                var metric = new DayWeekClass
                {
                    Number = count,
                    Day = ix.Cell(i, column).GetValue<string>()
                };
                count++;
                dayWeeks.Add(metric);
            }
            return dayWeeks;
        }

        public static int IndexGroup(string group, IXLWorksheet ix)
        {
            int columnsCount = ix.ColumnCount();
            for (int i = 1; i < columnsCount; i++)
            {
                if (ix.Cell(5, i).GetValue<string>() == group)
                {
                    return i;
                }
                else
                    continue;
            }
            return 0;
        }

        public static List<DayWeekClass> raspisaniekab(int row, string kabinet, IXLWorksheet ix) //Метод для возвращения расписания по кабинетам из xlsx
        {
            bool exit = false;
            int number = 1;
            int columnsCount = ix.ColumnsUsed().Count();
            List<DayWeekClass> kabinets = new List<DayWeekClass>();
            try
            {
                for (int i = row; i < row + 6; i++)
                {
                    for (int j = 3; j <= columnsCount; j++)
                    {
                        string result = ix.Cell(i, j).GetValue<string>();
                        if (result.Contains(kabinet))
                        {
                            kabinets.Add(new DayWeekClass { Number = number, cabinet = kabinet, Day = result + $"\n{ix.Cell(5, j).GetValue<string>()}" });
                            exit = false;
                            break;
                        }
                        else
                            exit = true;
                    }
                    if (exit)
                        kabinets.Add(new DayWeekClass { Number = number, Day = "-", cabinet = kabinet });
                    number++;
                }
            }
            catch { }
            return kabinets;
        }

        public static List<DayWeekClass> raspisanieteach(int row, string teach, IXLWorksheet ix) //Метод для возвращения расписания по преподавтелям из xlsx
        {
            bool exit = false;
            int number = 1;
            int columnsCount = ix.ColumnsUsed().Count();
            List<DayWeekClass> kabinets = new List<DayWeekClass>();
            for (int i = row; i < row + 6; i++)
            {
                for (int j = 3; j <= columnsCount; j++)
                {
                    string result = ix.Cell(i, j).GetValue<string>();
                    if (result.Contains(teach))
                    {
                        kabinets.Add(new DayWeekClass { Number = number, Day = result + $"\n{ix.Cell(5, j).GetValue<string>()}" });
                        exit = false;
                        break;
                    }
                    else
                        exit = true;
                }
                if (exit)
                    kabinets.Add(new DayWeekClass { Number = number, Day = "-" });
                number++;
            }
            return kabinets;
        }
    }
}
