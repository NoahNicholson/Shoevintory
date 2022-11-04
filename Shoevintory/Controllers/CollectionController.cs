using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Shoevintory.Models;
using Shoevintory.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using Collection = Shoevintory.Models.Collection;

namespace Shoevintory.Controllers
{
    public class CollectionController : Controller
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IShoeRepository _shoeRepository;
        private readonly IShoeCollectionRepository _shoeCollectionRepository;
        public CollectionController(ICollectionRepository collectionRepository, IShoeRepository shoeRepository, IShoeCollectionRepository shoeCollectionRepository)
        {
            _collectionRepository = collectionRepository;
            _shoeRepository = shoeRepository;
            _shoeCollectionRepository = shoeCollectionRepository;
        }
        // GET: CollectionController
        public ActionResult Index()
        {
            string userProfileId = HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            List<Collection> collections = _collectionRepository.GetAllCollections(int.Parse(userProfileId));

          
            return View(collections);
        }

        // GET: CollectionController/Details/5
        public ActionResult Details(int id)
        {
            List<UserShoeViewModel> usershoes = _shoeCollectionRepository.GetAllUserShoes(id);
            Collection collection =_collectionRepository.GetCollectionsById(id);
            var vm = new CollectionDetailsViewModel { CollectionId = id, Shoes = usershoes.ToList(), CollectionName = collection.Name };

            return View(vm);
           
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

                return RedirectToAction("Index");
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
