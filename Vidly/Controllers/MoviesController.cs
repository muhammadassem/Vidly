using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private MyDbContext _context;
        public MoviesController()
        {
            this._context = new MyDbContext();   
        }
        // GET: Movies
        public ActionResult Index()
        {
            //var movies = this._context.Movies.Include(m => m.Genre).ToList();
            //return View(movies);
            return View(); // render on client
        }

        public ActionResult Random()
        {
            var movie = new Movie() { Name= "film"};
            //return View(movie);
            //return Content("movie");
            //return HttpNotFound();
            //return Redirect("https://www.facebook.com/almawkef.almasry/photos/a.1777573749009181/1777574069009149/?type=3&theater");
            return RedirectToAction("index", "Home", new { page = 1, sort = "sorted" });
        }

        public ActionResult Edit (int id)
        {
            var movie = this._context.Movies.Single(m => m.Id == id);

            var viewModel = new MovieFormViewModel(movie)
            {
                GenreList = this._context.Genries.ToList()
            };


            return View("MovieForm", viewModel);
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content("hello");
        }

        public ActionResult ShowData()
        {
            var v = new MoviesViewModel() 
            {
                Customers = new List<Customer>
                {
                    new Customer() {Name="Customer 1"},
                    new Customer() {Name="Customer 2"},
                    new Customer() {Name="Customer 3"}
                },
                Movie = new Movie() { Name= "Movie Name"}
            };

            
            return View(v);
        }

        public ActionResult Details(int id)
        {
            var movie = this._context.Movies.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);

            if(movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie) // or MovieFormViewModel .. but entity framework is intilligint to map form object to movie model dirctly
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    GenreList = this._context.Genries.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if(movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                this._context.Movies.Add(movie);
            } else
            {
                var movieInDB = this._context.Movies.Single(m => m.Id == movie.Id);

                movieInDB.Name = movie.Name;
                movieInDB.NumberInStock = movie.NumberInStock;
                movieInDB.GenreId = movie.GenreId;
                movieInDB.ReleaseDate = movie.ReleaseDate;
            }

            try
            {
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                Console.WriteLine(e.Message);
            }


            return RedirectToAction("Index", "Movies");
        }

        public ActionResult New()
        {
            var genreList = this._context.Genries.ToList();

            var viewModel = new MovieFormViewModel()
            {
                GenreList = genreList
            };

            return View("MovieForm", viewModel);
        }
    }
}