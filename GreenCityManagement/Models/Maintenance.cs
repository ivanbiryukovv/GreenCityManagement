using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenCityManagement.Models
{
    using System;

    public class Maintenance
    {
        public int ID_maintenance { get; set; }
        public int ID_plant { get; set; }
        public int ID_work_type { get; set; }
        public DateTime Work_date { get; set; }
        public string Result { get; set; }
    }

}
