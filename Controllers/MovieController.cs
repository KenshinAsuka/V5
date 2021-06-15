using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using V5.Models;
using PagedList;
using PagedList.Mvc;
using cloudscribe.Pagination.Models;

namespace V5.Controllers
{
    [Route("movie")]
    public class MovieController : Controller
    {
        private MvcMovieContextcs db = new MvcMovieContextcs();


        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index(string searchString, string sortOrder, int pageNumber = 1, int pageSize = 2)
        {

            int excRecords = (pageSize * pageNumber) - pageSize;

            var movie = db.Movie.OrderBy(s => s.Id).Skip(excRecords).Take(pageSize);


            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["TitleSortParam"] = sortOrder == "Title" ? "title_desc" : "Title";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["GenreSortParam"] = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewData["PriceSortParam"] = sortOrder == "Price" ? "price_desc" : "Price";

           

            switch (sortOrder)
            {
                case "date_desc":
                    movie = db.Movie.OrderByDescending(s => s.ReleaseDate).Skip(excRecords).Take(pageSize);
                    break;
                case "Date":
                    movie = db.Movie.OrderBy(s => s.ReleaseDate).Skip(excRecords).Take(pageSize);
                    break;
                case "title_desc":
                    movie = db.Movie.OrderByDescending(s => s.Title).Skip(excRecords).Take(pageSize);
                    break;
                case "Title":
                    movie = db.Movie.OrderBy(s => s.Title).Skip(excRecords).Take(pageSize);
                    break;
                case "genre_desc":
                    movie = db.Movie.OrderByDescending(s => s.Genre).Skip(excRecords).Take(pageSize);
                    break;
                case "Genre":
                    movie = db.Movie.OrderBy(s => s.Genre).Skip(excRecords).Take(pageSize);
                    break;
                case "price_desc":
                    movie = db.Movie.OrderByDescending(s => s.Price).Skip(excRecords).Take(pageSize);
                    break;
                case "Price":
                    movie = db.Movie.OrderBy(s => s.Price).Skip(excRecords).Take(pageSize);
                    break;
                default:
                    movie = db.Movie.OrderBy(s => s.Id).Skip(excRecords).Take(pageSize);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                movie = db.Movie.Where(s => s.Title.Contains(searchString));
            }

            var result = new PagedResult<Movie>
            {
                Data = movie.ToList(),
                TotalItems = db.Movie.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            //ViewBag.movies = result;

            return View(result);
        }

        [Route("Create")]
        public IActionResult Create()
        {

            return View("Create");
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(Movie movie)
        {
            db.Movie.Add(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            db.Movie.Remove(db.Movie.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            return View("Edit", db.Movie.Find(id));
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public IActionResult Edit(Movie movie)
        {
            db.Entry(movie).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}