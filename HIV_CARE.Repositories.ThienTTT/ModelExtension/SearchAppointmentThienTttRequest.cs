using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIV_CARE.Repositories.ThienTTT.ModelExtension
{
    public class SearchAppointmentThienTttRequest
    {
        public int? CurrentPage { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }

    public class SearchByName : SearchAppointmentThienTttRequest
    {
        public int? appointmetnId { get; set; }
        public DateTime? date { get; set; }
        public int? doctorId { get; set; }
    }

    //public class SearchByName()
    //{

    //}


}
