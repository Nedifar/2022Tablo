using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloBlazorMain.Shared.LastDanceModels
{
    public class PostFloorModel
    {
        public string floor { get; set; }
        public int count { get; set; }
        public string paraNow { get; set; }
        public List<Models.Para> CHKR { get; set; }
    }
}
