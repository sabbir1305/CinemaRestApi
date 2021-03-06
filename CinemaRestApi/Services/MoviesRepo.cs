using CinemaRestApi.Data;
using CinemaRestApi.Models;
using CinemaRestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaRestApi.Services
{
    public class MoviesRepo : IMovie
    {
        private CinemaDbContext _dbContext;

        public MoviesRepo(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddMovie(Movie movie)
        {
            _dbContext.Movies.Add(movie);
        }

        public void Delete(Movie movie)
        {
            _dbContext.Movies.Remove(movie);
        }

        public IList<MovieDto> GetAllMovies(SearchParamDto search)
        {
            var query = _dbContext.Movies.Select(x =>
             new MovieDto
             {
                 Id = x.Id,
                 Duration = x.Duration,
                 Genre = x.Genre,
                 ImageUrl = x.ImageUrl,
                 Language = x.Language,
                 Name = x.Name,
                 Rating = x.Rating
             });
            int pageSize = search.pageSize ?? 5;
           int pageNumber = search.pageNumber ?? 1;

            switch (search.sort)
            {
                case "desc":
                    query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderByDescending(x => x.Rating);
                    break;
                case "asc":
                    query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(x => x.Rating);
                    break;
                default:
                    query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    break;
            }

            return query.ToList();
        }

        public Movie MovieDetail(int id)
        {
            var movie = _dbContext.Movies.Find(id);
            return movie;
        }

        public bool Save()
        {
          return  _dbContext.SaveChanges() >= 0;
        }

        public string SaveImage(IFormFile Image)
        {
           
                var imgId = Guid.NewGuid();
                var imgUrl = imgId + ".jpg";
                var filePath = Path.Combine("wwwroot", imgUrl);
                if (Image != null)
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(fileStream);
                    }
                }
            return imgUrl;
         

        }

    
    }
}
