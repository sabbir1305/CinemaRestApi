using CinemaRestApi.Data;
using CinemaRestApi.Models;
using CinemaRestApi.Services.Reservations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private CinemaDbContext _dbContext;
        private IReservation _repo;
        public ReservationsController(CinemaDbContext dbContext, IReservation reservation)
        {
            _dbContext = dbContext;
            _repo = reservation;
        }
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]Reservation reservation)
        {
            _repo.Add(reservation);
            _repo.Save();
            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize("Admin")]
        [HttpGet]
        public IActionResult GetReservations()
        {
            var reservationLst = _repo.GetReservations();
            return Ok(reservationLst);
        }

        [Authorize("Admin")]
        [HttpGet]
        public IActionResult GetReservationDetail(int id)
        {
            var reservation = _repo.GetReservationDetail(id);
            return Ok(reservation);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reservation = _repo.GetReservation(id);
            if (reservation == null)
            {
                return NotFound("No record found against this id");
            }

            _repo.Delete(reservation);
            _repo.Save();

            return Ok("Record Deleted");
        }


    }
}
