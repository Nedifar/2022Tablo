using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloBlazorMain.Shared.Models
{
    public class MonthYear
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idMonthYear { get; set; }
        public DateTime date { get; set; }
        public virtual List<SupervisorShedule> SupervisorShedules { get; set; } = new List<SupervisorShedule>();
        public string getSupervisorNow
        {
            get
            {
                try
                {
                    foreach(var item in SupervisorShedules)
                    {
                        switch (DateTime.Now.Day)
                        {
                            case 1:
                                var i1 =  item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i1.d1)
                                    return i1.SupervisorShedule.NameSupervisor;
                                break;
                            case 2:
                                var i2 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i2.d2)
                                    return i2.SupervisorShedule.NameSupervisor;
                                break;
                            case 3:
                                var i3 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i3.d3)
                                    return i3.SupervisorShedule.NameSupervisor;
                                break;
                            case 4:
                                var i4 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i4.d4)
                                    return i4.SupervisorShedule.NameSupervisor;
                                break;
                            case 5:
                                var i5 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i5.d5)
                                    return i5.SupervisorShedule.NameSupervisor;
                                break;
                            case 6:
                                var i6 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i6.d6)
                                    return i6.SupervisorShedule.NameSupervisor;
                                break;
                            case 7:
                                var i7 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i7.d7)
                                    return i7.SupervisorShedule.NameSupervisor;
                                break;
                            case 8:
                                var i8 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i8.d8)
                                    return i8.SupervisorShedule.NameSupervisor;
                                break;
                            case 9:
                                var i9 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i9.d9)
                                    return i9.SupervisorShedule.NameSupervisor;
                                break;
                            case 10:
                                var i10 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i10.d10)
                                    return i10.SupervisorShedule.NameSupervisor;
                                break;
                            case 11:
                                var i11 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i11.d11)
                                    return i11.SupervisorShedule.NameSupervisor;
                                break;
                            case 12:
                                var i12 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i12.d12)
                                    return i12.SupervisorShedule.NameSupervisor;
                                break;
                            case 13:
                                var i13 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i13.d13)
                                    return i13.SupervisorShedule.NameSupervisor;
                                break;
                            case 14:
                                var i14 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i14.d14)
                                    return i14.SupervisorShedule.NameSupervisor;
                                break;
                            case 15:
                                var i15 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i15.d15)
                                    return i15.SupervisorShedule.NameSupervisor;
                                break;
                            case 16:
                                var i16 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i16.d16)
                                    return i16.SupervisorShedule.NameSupervisor;
                                break;
                            case 17:
                                var i17 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i17.d17)
                                    return i17.SupervisorShedule.NameSupervisor;
                                break;
                            case 18:
                                var i18 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i18.d18)
                                    return i18.SupervisorShedule.NameSupervisor;
                                break;
                            case 19:
                                var i19 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i19.d19)
                                    return i19.SupervisorShedule.NameSupervisor;
                                break;
                            case 20:
                                var i20 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i20.d20)
                                    return i20.SupervisorShedule.NameSupervisor;
                                break;
                            case 21:
                                var i21 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i21.d21)
                                    return i21.SupervisorShedule.NameSupervisor;
                                break;
                            case 22:
                                var i22 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i22.d22)
                                    return i22.SupervisorShedule.NameSupervisor;
                                break;
                            case 23:
                                var i23 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i23.d23)
                                    return i23.SupervisorShedule.NameSupervisor;
                                break;
                            case 24:
                                var i24 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i24.d24)
                                    return i24.SupervisorShedule.NameSupervisor;
                                break;
                            case 25:
                                var i25 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i25.d25)
                                    return i25.SupervisorShedule.NameSupervisor;
                                break;
                            case 26:
                                var i26 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i26.d26)
                                    return i26.SupervisorShedule.NameSupervisor;
                                break;
                            case 27:
                                var i27 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i27.d27)
                                    return i27.SupervisorShedule.NameSupervisor;
                                break;
                            case 28:
                                var i28 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i28.d28)
                                    return i28.SupervisorShedule.NameSupervisor;
                                break;
                            case 29:
                                var i29 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i29.d29)
                                    return i29.SupervisorShedule.NameSupervisor;
                                break;
                            case 30:
                                var i30 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i30.d30)
                                    return i30.SupervisorShedule.NameSupervisor;
                                break;
                            case 31:
                                var i31 = item.DatesSupervisiors.Where(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month).FirstOrDefault();
                                if (i31.d31)
                                    return i31.SupervisorShedule.NameSupervisor;
                                break;
                            
                                
                        }
                        
                    }
                    return "";
                }
                catch
                { return ""; }
            }
        }
    }
}
