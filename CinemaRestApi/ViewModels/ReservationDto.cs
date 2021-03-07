using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaRestApi.ViewModels
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }

        public string Phone { get; set; }

        public DateTime ReservationTime { get; set; }
        public DateTime PlayingDate { get; set; }
        public DateTime PlayingTime { get; set; }

        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
