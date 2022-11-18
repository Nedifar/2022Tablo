using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloBlazorMain.Shared.Models
{
    public class DatesSupervisior
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idDatesSupervisior { get; set; }
        public DateTime date { get; set; }
        [ForeignKey("SupervisorShedule")]
        public int idSupervisorShedule { get; set; }
        public virtual SupervisorShedule SupervisorShedule { get; set; }
        public bool d1 { get; set; }
        public bool d2 { get; set; }
        public bool d3 { get; set; }
        public bool d4 { get; set; }
        public bool d5 { get; set; }
        public bool d6 { get; set; }
        public bool d7 { get; set; }
        public bool d8 { get; set; }
        public bool d9 { get; set; }
        public bool d10 { get; set; }
        public bool d11 { get; set; }
        public bool d12 { get; set; }
        public bool d13 { get; set; }
        public bool d14 { get; set; }
        public bool d15 { get; set; }
        public bool d16 { get; set; }
        public bool d17 { get; set; }
        public bool d18 { get; set; }
        public bool d19 { get; set; }
        public bool d20 { get; set; }
        public bool d21 { get; set; }
        public bool d22 { get; set; }
        public bool d23 { get; set; }
        public bool d24 { get; set; }
        public bool d25 { get; set; }
        public bool d26 { get; set; }
        public bool d27 { get; set; }
        public bool d28 { get; set; }
        public bool d29 { get; set; }
        public bool d30 { get; set; }
        public bool d31 { get; set; }
    }
}
