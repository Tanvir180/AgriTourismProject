using AgriTourismProject.Data;
using AgriTourismProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgriTourismProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string searchString)
        {
            List<Category> objCategoryList;

            if (string.IsNullOrEmpty(searchString))
            {
                objCategoryList = _db.Categories.ToList();
            }
            else
            {
                objCategoryList = _db.Categories
                    .Where(c => c.Name.Contains(searchString) || c.Location.ToString().Contains(searchString))
                    .ToList();

                if (!objCategoryList.Any())
                {
                    TempData["warning"] = "No categories found matching your search criteria.";
                }
            }

            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            //}

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }



        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }





        public IActionResult Payment(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            if (category.Capacity > 0)
            {
                category.Capacity--;
                _db.SaveChanges();
                TempData["BookingSuccess"] = true;
                return RedirectToAction("PaymentConfirmation");
            }
            else
            {
                TempData["OutOfCapacity"] = true;
                return RedirectToAction("Details", new { id });
            }
        }

        public IActionResult PaymentConfirmation()
        {
            return View();
        }
    }



}




