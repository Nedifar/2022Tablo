using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloBlazorMain.Shared.Models
{
    public class DayPartHeader
    {
        public int DayPartHeaderId { get; set; } 
        public string Header { get; set; } = "Memories";
        public DateTime beginTime { get; set; }
        public DateTime endTime { get; set; }
    }
}
