using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloBlazorMain.Shared.Models;
using TabloBlazorMain.Shared.LastDanceModels;

namespace TabloBlazorMain.Server.Context
{
    public class context : DbContext
    {
        public context(DbContextOptions<context> options) : base(options) { }
        public DbSet<WeekName> WeekNames { get; set; }
        public DbSet<TimeShedule> TimeShedules { get; set; }
        public DbSet<SupervisorShedule> SupervisorShedules { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<DatesSupervisior> DatesSupervisiors { get; set; }
        public DbSet<MonthYear> MonthYear { get; set; }
        public DbSet<DayPartHeader> DayPartHeaders { get; set; }
        public DbSet<SpecialDayWeekName> SpecialDayWeekNames { get; set; }
        public DbSet<TypeInterval> TypeIntervals { get; set; }
        public DbSet<Para> Paras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns();
        }
    }
}
