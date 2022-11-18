using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TabloBlazorMain.Shared.Models
{
    public class Announcement
    {
        [Key]
        public int idAnnouncement { get; set; }
        public string Header { get; set; }
        public string Name { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime? dateClosed { get; set; }
        public string Priority { get; set; }
        public bool isActive { get; set; }
        public string status { get; set; }
    }
}
