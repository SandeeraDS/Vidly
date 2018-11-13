using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        VidlyContext _context;

        public MoviesController()
        {
            _context = new VidlyContext();
        }

        [HttpGet]
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>); ;
        }

        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Mapper.Map<Movie,MovieDto>(movie));
            }
        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var movie= Mapper.Map<MovieDto, Movie>(movieDto);
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return Created(new Uri(Request.RequestUri+"/"+movie.Id),movieDto);
            }
        }

        [HttpPut]
        public void UpdateMovie(int id,MovieDto movieDto) {

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
               var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
                if (movieInDb == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    Mapper.Map(movieDto, movieInDb);
                    _context.SaveChanges();
                }
            }

        }

        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);

            }
            else
            {
                _context.Movies.Remove(movieInDb);
                _context.SaveChanges();
            }
        }

    }
}
