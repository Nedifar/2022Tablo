using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloBlazorMain.Shared.Models
{
    public class SpecialDayWeekName
    {
        [Key]
        public int idSpecialDayWeekName { get; set; }
        public int dayWeek { get; set; }
    }
}
