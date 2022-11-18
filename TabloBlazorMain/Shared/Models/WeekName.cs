using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TabloBlazorMain.Shared.Models
{
    public class WeekName
    {
        [Key]
        public int idWeekName { get; set; }
        public DateTime Begin { get; set; }
        public string Name { get; set; }
    }
}
