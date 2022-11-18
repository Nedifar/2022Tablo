using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloBlazorMain.Shared.LastDanceModels
{
    public class FloorCabinet
    {
        public string Name { get; set; }
        public List<DayWeekClass> DayWeeks { get; set; } = new List<DayWeekClass>();
    }
}
