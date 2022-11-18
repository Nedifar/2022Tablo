using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TabloBlazorMain.Shared.LastDanceModels
{
    public class DayWeekClass
    {
        public int? Number { get; set; }
        public string Day { get; set; }
        public string Date { get; set; }
        public string cabinet { get; set; }
        public string statusPara { get; set; }
        
        public string decipline
        {
            get
            {
                string result = Day?.Split('\n')[0];
                if (result != null)
                {
                    if (result.Contains("ДОП"))
                        return "ДОП";                       
                }
                return result;
            }
        }

        public string GetDeciplineWithVerify(List<string> teachers)
        {
            if (teachers == null)
                return decipline;
            string verifyDecipline = decipline;
            foreach (string teacher in teachers)
            {
                if (decipline != null)
                {
                    if (decipline.Contains(teacher))
                    {
                        verifyDecipline = decipline?.Replace(teacher, "").Trim();
                    }
                }
            }
            return verifyDecipline;
        }

        public string dec1
        {
            get; set;
        }
        public string gr1
        {
            get; set;
        }
        public string group
        {
            get
            {
                return Day?.Split('\n').LastOrDefault();
            }
        }
        public string teacher(List<string> teachers)
        {
            if (teachers == null)
                return "-";
            foreach (var teacher in teachers)
            {
                if (Day != null)
                {
                    if (Day.Contains(teacher))
                    {
                        return teacher;
                    }
                }
            }
            return "-";
        }

        public string pp
        {
            get; set;
        }
    }
}
