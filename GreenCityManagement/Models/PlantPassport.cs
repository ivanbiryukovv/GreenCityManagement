using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenCityManagement.Models
{
    using System;

    public class PlantPassport
    {
        public int ID_passport { get; set; }
        public int ID_plant { get; set; }
        public decimal Height { get; set; }
        public int Age { get; set; }
        public DateTime Last_inspection_date { get; set; }
    }

}
