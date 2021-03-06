using CinemaRestApi.Data;
using CinemaRestApi.Models;
using CinemaRestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaRestApi.Services
{
   public interface IMovie
    {

        IList<MovieDto> GetAllMovies(SearchParamDto search);
        IList<MovieFindDto> FindMovies(string name);
        Movie MovieDetail(int id);

        void AddMovie(Movie movie);

        void Delete(Movie movie);

        bool Save();
        string SaveImage(IFormFile Image);
    }
}
