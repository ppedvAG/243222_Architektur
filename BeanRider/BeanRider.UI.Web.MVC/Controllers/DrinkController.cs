using BeanRider.Model.Contracts;
using BeanRider.Model.DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeanRider.UI.Web.MVC.Controllers
{
    public class DrinkController : Controller
    {
        IRepository repo;

        public DrinkController(IRepository repo)
        {
            this.repo = repo;
        }

        // GET: DrinkController
        public ActionResult Index()
        {
            return View(repo.GetAll<Drink>());
        }

        // GET: DrinkController/Details/5
        public ActionResult Details(int id)
        {
            return View(repo.GetById<Drink>(id));
        }

        // GET: DrinkController/Create
        public ActionResult Create()
        {
            return View(new Drink() { Name = "Neu", Price = 4 });
        }

        // POST: DrinkController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Drink drink)
        {
            try
            {
                repo.Add(drink);
                repo.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DrinkController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(repo.GetById<Drink>(id));
        }

        // POST: DrinkController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Drink drink)
        {
            try
            {
                repo.Update(drink);
                repo.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DrinkController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repo.GetById<Drink>(id));
        }

        // POST: DrinkController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Drink drink)
        {
            try
            {
                repo.Delete(drink);
                repo.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
