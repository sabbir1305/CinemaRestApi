using CinemaRestApi.Data;
using CinemaRestApi.Models;
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
    public class MoviesController : ControllerBase
    {


        private CinemaDbContext _dbContext;

        public MoviesController(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult  Get()
        {
            //   return _dbContext.Movies;

            return StatusCode(StatusCodes.Status200OK,_dbContext.Movies);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var movie = _dbContext.Movies.Find(id);
           if (movie == null)
            {
                return NotFound("No record found against this id");
            }

            return Ok(movie) ;
        }


        [HttpPost]
        public IActionResult Post([FromBody]Movie movie)
        {
            _dbContext.Movies.Add(movie);

            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] Movie movie)
        {
           var dbMovie = _dbContext.Movies.Find(id);
            if (movie == null)
            {
                return NotFound("No record found against this id");
            }
            dbMovie.Name = movie.Name;
            dbMovie.Language = movie.Language;
            _dbContext.SaveChanges();
            return Ok("Record Updated Successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = _dbContext.Movies.Find(id);
            if (movie == null)
            {
                return NotFound("No record found against this id");
            }

            _dbContext.Remove(movie);
            _dbContext.SaveChanges();

            return Ok("Record Deleted");
        }
    }
}
