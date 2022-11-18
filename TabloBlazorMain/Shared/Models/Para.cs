using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TabloBlazorMain.Shared.Models
{
    public class Para
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int idPara { get; set; }
        public string Name { get; set; }
        public DateTime begin { get; set; }
        public DateTime end { get; set; }
        public int numberInList { get; set; }
        public int numberInterval { get; set; }
        [ForeignKey("TypeInterval")]
        public int idTypeInterval { get; set; }
        public virtual LastDanceModels.TypeInterval TypeInterval { get; set; }
        [ForeignKey("TimeShedule")]
        public int idTimeShedule { get; set; }
        [JsonIgnore]
        public virtual TimeShedule TimeShedule { get; set; }

        public string labelPara
        {
            get
            {
                return Name;
            }
        }

        public string outGraphicNewTablo
        {
            get
            {
                return TypeInterval.name switch
                {
                    "Перемена" => "П",
                    "Пара" => numberInterval.ToString(),
                    "Событие" => Name,
                    "ЧКР" => "ЧКР"
                };
            }
        }

        public double toEndTimeInProcent
        {
            get
            {
                return 100-100 * (end.TimeOfDay.TotalMinutes - DateTime.Now.TimeOfDay.TotalMinutes) / height;
            }
        }

        public string toEndTime
        {
            get
            {
                double b = Math.Ceiling(end.TimeOfDay.TotalMinutes - DateTime.Now.TimeOfDay.TotalMinutes);
                return TimeToHourAndMinute(b, Math.Truncate(b / 60));
            }
        }

        private string TimeToHourAndMinute(double minute, double hour)
        {
            if (minute - hour * 60 == 0)
            {
                return hour + " час ";
            }
            else if (hour != 0)
            {
                return hour + " час " + (minute - hour * 60) + " мин.";
            }
            else
                return minute + " мин.";
        }
        public bool runningNow
        {
            get
            {
                if (toEndTime.Contains("-"))
                {
                    return true;
                }
                else { return false; }
            }
        }
        public string beginEnd
        {
            get
            {
                return begin.ToString("HH.mm") + "-" + end.ToString("HH.mm");
            }
        }
        public string drHeight()
        {
            string result = height + Main.height + "";
            return result;
        }
        public double height
        {
            get
            {
                return totalTime;
            }
        }
        public double heightText
        {
            get
            {
                if (totalTime == 5)
                    return 2.5 * totalTime;
                return 1.5 * totalTime;
            }
        }
        public string fontSize
        {
            get
            {
                if (TypeInterval.name =="Пара")
                {
                    return "3.5em";
                }
                else
                    return "1em";
            }
        }
        public double totalTime
        {
            get
            {
                return (end.TimeOfDay - begin.TimeOfDay).TotalMinutes;
            }

        }
    }
}
