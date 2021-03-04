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
        private static List<Movie> movies = new List<Movie>
        {
            new Movie(){ Id=0,Name="Mission Impossible",Language="English"},
            new Movie(){ Id=1,Name="Mission Impossible2",Language="English"}
        };

        private CinemaDbContext _dbContext;

        public MoviesController(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return _dbContext.Movies;
        }

        [HttpGet("{id}")]
        public Movie Get(int id)
        {
            return _dbContext.Movies.Find(id);
        }


        [HttpPost]
        public void Post([FromBody]Movie movie)
        {
            _dbContext.Movies.Add(movie);

            _dbContext.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id,[FromBody] Movie movie)
        {
           var dbMovie = _dbContext.Movies.Find(id);
            dbMovie.Name = movie.Name;
            dbMovie.Language = movie.Language;
            _dbContext.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var movie = _dbContext.Movies.Find(id);
            _dbContext.Remove(movie);
            _dbContext.SaveChanges();
        }
    }
}
