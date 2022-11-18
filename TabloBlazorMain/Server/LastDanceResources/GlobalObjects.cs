using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloBlazorMain.Shared.LastDanceModels;
using TabloBlazorMain.Shared.Models;

namespace TabloBlazorMain.Server.LastDanceResources
{
    public class GlobalObjects
    {
        public static string weather { get; set; }
        public static RequestForDynamicUpdate request { get; set; }
        public static List<DayWeekClass> cabinets { get; set; }
    }
}
