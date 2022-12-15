using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataFile;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        // How dependency injection works. No need to create a new obj,
        // just call service for SQL that's set up in Program.cs and configured in appsettings.json
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            // Access DB. Grab categories and pass them to view 
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        // GET
        // When user clicks create button, code executes and creates new category
        public IActionResult Create()
        {
            return View();
        }

        // POST
        // When user clicks create button, code executes and creates new category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // The "name" arg refers to the 'Name' field in the Category class. Will display error under that input field
                ModelState.AddModelError("name", "The Display Order cannot exactly match Name.");
            }

            // Validation to check all annotations in Category model are satisfied
            if (ModelState.IsValid)
            {
                // Starts tracking the new data
                _db.Categories.Add(obj);
                // Saves change to data base
                _db.SaveChanges();

                // TempData allows you to display alert messages. Will only be created an retrievable for a single redirect
                // then disappears after next action
                TempData["success"] = "Category created successfully!";

                // RedirectToAction executes an method in Controller class specified (method name is "Index")
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();

            }
            return View(categoryFromDb);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match Name.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category edited successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();

            }
            return View(categoryFromDb);
        }


        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();

            }
            _db.Categories.Remove(categoryFromDb);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }

    }
}
