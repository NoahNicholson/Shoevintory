using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoevintory.Models;
using Shoevintory.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Shoevintory.Controllers
{
    public class ShoeController : Controller
    {
        private readonly IShoeRepository _shoeRepository;
        public ShoeController(IShoeRepository shoeRepository)
        {
            _shoeRepository = shoeRepository;
        }
        // GET: ShoeController

        public ActionResult Index()
        {
            List<Shoe> shoes = _shoeRepository.GetAllShoes();

            return View(shoes);
        }

        // GET: ShoeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: ShoeController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: ShoeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Shoe shoe)
        {
            try
            {
                _shoeRepository.Create(shoe);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoeController/Edit/5
        public ActionResult Edit(int id)
        {
            var shoe = _shoeRepository.GetShoeById(id);

            return View(shoe);
        }

        // POST: ShoeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm] Shoe shoe)
        {
            try
            {
                _shoeRepository.Edit(shoe);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoeController/Delete/5
        //public ActionResult Delete(int id)
        //{

        //    return View();
        //}

        // POST: ShoeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _shoeRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
