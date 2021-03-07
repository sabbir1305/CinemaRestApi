using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaRestApi.ViewModels
{
    public class ReservationSummaryDto
    {
        public int Id { get; set; }

        public DateTime ReservationTime { get; set; }


        public string MovieName { get; set; }
 
        public string UserName { get; set; }
    }
}

