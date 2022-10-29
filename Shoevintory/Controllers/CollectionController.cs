using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoevintory.Models;
using Shoevintory.Repositories;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;

namespace Shoevintory.Controllers
{
    public class CollectionController : Controller
    {
        private readonly ICollectionRepository _collectionRepository;
        public CollectionController(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }
        // GET: CollectionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CollectionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CollectionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CollectionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(Collection collection)
        {
            try
            {

                string userProfileId = HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
                collection.UserProfileId = int.Parse(userProfileId);
                _collectionRepository.Create(collection);

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: CollectionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CollectionController/Edit/5
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

        // GET: CollectionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CollectionController/Delete/5
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
