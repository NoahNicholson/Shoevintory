using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shoevintory.Models;
using Shoevintory.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Shoevintory.Controllers
{
    public class ShoeCollectionController : Controller
    {
        private readonly IShoeRepository _shoeRepository;
        private readonly IShoeCollectionRepository _shoeCollectionRepository;

        public ShoeCollectionController(IShoeRepository shoeRepository, IShoeCollectionRepository shoeCollectionRepository)
        {
            _shoeRepository = shoeRepository;
            _shoeCollectionRepository = shoeCollectionRepository;
        }

        // GET: ShoeCollectionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ShoeCollectionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShoeCollectionController/Create
        [HttpGet("/collection/{id}/shoes")]
        public ActionResult Create(int id)
        {
            List<Shoe> allshoes = _shoeRepository.GetAllShoes();
            var items = allshoes.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() });
            var vm = new AddShoeToCollectionViewModel { CollectionId = id, Shoes = items.ToList() };
            return View(vm);
        }

        // POST: ShoeCollectionController/Create
        [HttpPost("/collection/{id}/shoes")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddShoeToCollectionViewModel vm)
        {
            try
            {
                _shoeCollectionRepository.Create(new ShoeCollection {ShoeId = vm.SelectedShoe, CollectionId = vm.CollectionId});

                return RedirectToAction("Details", "Collection", new { id = vm.CollectionId });
            }
            catch
            {
                return View(vm);
            }
        }

        // GET: ShoeCollectionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShoeCollectionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoeCollectionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShoeCollectionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
