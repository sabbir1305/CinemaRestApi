using CinemaRestApi.Models;
using CinemaRestApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaRestApi.Services.Reservations
{
   public interface IReservation
    {
        void Add(Reservation reservation);
        IList<ReservationSummaryDto> GetReservations();
        ReservationDto GetReservationDetail(int id);

        Reservation GetReservation(int id);

        void Delete(Reservation reservation);
        bool Save();
    }
}
