using CinemaRestApi.Data;
using CinemaRestApi.Models;
using CinemaRestApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaRestApi.Services.Reservations
{
    public class ReservationRepo : IReservation
    {
        private CinemaDbContext _dbContext;

        public ReservationRepo(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Reservation reservation)
        {
            reservation.ReservationTime = DateTime.Now;
            _dbContext.Add(reservation);
        }

        public void Delete(Reservation reservation)
        {
            _dbContext.Remove(reservation);
        }

        public Reservation GetReservation(int id)
        {
            return _dbContext.Reservations.Find(id);
        }

        public ReservationDto GetReservationDetail(int id)
        {
            var data = (from rs in _dbContext.Reservations
                        join m in _dbContext.Movies on rs.MovieId equals m.Id
                        join u in _dbContext.Users on rs.UserId equals u.Id
                        where rs.Id==id
                        select new ReservationDto
                        {
                            Id = rs.Id,
                            MovieName = m.Name,
                            ReservationTime = rs.ReservationTime,
                            UserName = u.Name,
                            MovieId=m.Id,
                            Phone=rs.Phone,
                            Price=rs.Price,
                            Qty=rs.Qty,
                            UserId=rs.UserId,
                            PlayingDate=m.PlayingDate,
                            PlayingTime=m.PlayingTime
                        }).FirstOrDefault();

            return data;
        }

        public IList<ReservationSummaryDto> GetReservations()
        {
            var data = (from rs in _dbContext.Reservations
                        join m in _dbContext.Movies on rs.MovieId equals m.Id
                        join u in _dbContext.Users on rs.UserId equals u.Id
                        select new ReservationSummaryDto
                        {
                            Id = rs.Id,
                            MovieName = m.Name,
                            ReservationTime = rs.ReservationTime,
                            UserName = u.Name
                        }).ToList();

            return data;
        }

        public bool Save()
        {
           return _dbContext.SaveChanges() >= 0;
        }
    }
}
