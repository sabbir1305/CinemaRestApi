using CinemaRestApi.Data;

using CinemaRestApi.Models;
using CinemaRestApi.Services;
using CinemaRestApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CinemaRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private CinemaDbContext _dbContext;
        private IMovie _movieRepo;
        private readonly IActionContextAccessor _accessor;

        public MoviesController(CinemaDbContext dbContext,IMovie movie, IActionContextAccessor accessor)
        {
            _dbContext = dbContext;
            _movieRepo = movie;
            _accessor = accessor;
            //_movieRepo.SetDbContext(_dbContext);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Post([FromForm] Movie movie)
        {
          
            if (movie.Image != null)
            {
                movie.ImageUrl = _movieRepo.SaveImage(movie.Image);
            }

            _movieRepo.AddMovie(movie);
            _movieRepo.Save();


         
            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] Movie movie)
        {
            var dbMovie = _movieRepo.MovieDetail(id);
            if (movie == null)
            {
                return NotFound("No record found against this id");
            }

          
            if (movie.Image != null)
            {
           
                    dbMovie.ImageUrl = _movieRepo.SaveImage(movie.Image);
           
            }



            dbMovie.Name = movie.Name;
            dbMovie.Description = movie.Description;
            dbMovie.Duration = movie.Duration;
            dbMovie.Language = movie.Language;
            dbMovie.Rating = movie.Rating;
            dbMovie.Genre = movie.Genre;
            dbMovie.PlayingDate = movie.PlayingDate;
            dbMovie.PlayingTime = movie.PlayingTime;
            dbMovie.TicketPrice = movie.TicketPrice;
            _dbContext.SaveChanges();
            return Ok("Record Updated Successfully.");
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult AllMovies([FromQuery]SearchParamDto search)
        {
            var allMovies = _movieRepo.GetAllMovies(search);
            return Ok(allMovies);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult MovieDetail(int id)
        {
            var detail = _movieRepo.MovieDetail(id);
            if (detail == null)
            {
                return NotFound("No record found against this id");
            }

            return Ok(detail);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = _movieRepo.MovieDetail(id);
            if (movie == null)
            {
                return NotFound("No record found against this id");
            }

            _movieRepo.Delete(movie);
            _movieRepo.Save();

            return Ok("Record Deleted");
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult FindMovies(string movieName)
        {
            var allMovies = _movieRepo.FindMovies(movieName);
            return Ok(allMovies);
        } 


        //[HttpGet]
        //    public  string GetIP()
        //    {
        //    var ip = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
        //    return ip;
        //    }

        //[HttpGet]
        //public string GetIP2()
        //{
        //    var ip = HttpContext.Connection.RemoteIpAddress.ToString();
        //    return ip;
        //}

        [HttpGet("[action]")]
        public IActionResult GetLocationInfo()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ipapi.co/8.8.8.8/json/");
            request.UserAgent = "ipapi.co/#c-sharp-v1.01";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var reader = new System.IO.StreamReader(response.GetResponseStream(), UTF8Encoding.UTF8);
            return Ok(reader.ReadToEnd());
        }



    }
}
