using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloBlazorMain.Shared.Models
{
    public class RequestForDynamicUpdate
    {
        public string timeNow { get; set; }
        public string labelPara { get; set; }
        public string toEndPara {get; set;}
        public double progressBarPara { get; set; }
        public double rightMargin { get; set; }
        public List<Para> lv { get; set; } = new List<Para>();
        public List<Para> lvBorder { get; set; } = new List<Para>();
        public List<Para> lvTime { get; set; } = new List<Para>();
        public double grLineHeight { get; set; }
        public double lineMarginTop { get; set; }
        public double colorLineHeight { get; set; }
        public string tbNumberPara { get; set; }
        public double lineMargin { get; set; }
        public string paraNow { get; set; }
    }
}
