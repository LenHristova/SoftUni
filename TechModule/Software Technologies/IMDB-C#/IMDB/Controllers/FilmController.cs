using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IMDB.Models;

namespace IMDB.Controllers
{
    [ValidateInput(false)]
    public class FilmController : Controller
    {
        private IMDBDbContext db = new IMDBDbContext();

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            var films = db.Films.ToList();
            return View(films);
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Film film)
        {
            if (!ModelState.IsValid)
            {
                return View(film);
            }

            db.Films.Add(film);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            var film = db.Films
                .FirstOrDefault(f => f.Id == id);

            if (film == null)
            {
                return HttpNotFound();
            }

            return View(film);           
        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(int? id, Film filmModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", id);
            }

            var film = db.Films.FirstOrDefault(f => f.Id == id);

            if (film == null)
            {
                return HttpNotFound();
            }

            film.Name = filmModel.Name;
            film.Genre = filmModel.Genre;
            film.Director = filmModel.Director;
            film.Year = filmModel.Year;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            var film = db.Films
                .FirstOrDefault(f => f.Id == id);

            if (film == null)
            {
                return HttpNotFound();
            }

            return View(film);            
        }

        [HttpPost]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            var film = db.Films.FirstOrDefault(f => f.Id == id);

            if (film == null)
            {
                return HttpNotFound();
            }

            db.Films.Remove(film);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}