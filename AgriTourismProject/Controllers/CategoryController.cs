using AgriTourismProject.Data;
using AgriTourismProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            var user = _db.Users.FirstOrDefault(); // Replace with logic to get the current user
            if (user == null)
            {
                return NotFound();
            }

            var paymentViewModel = new PaymentViewModel
            {
                UserId = user.Id,
                UserName = user.Name,
                UserEmail = user.Email,
                UserPhoneNumber = user.PhoneNumber,
                UserAddress = user.Address,
                Id = category.Id,
                PlaceName = category.Name,
                Location = category.Location,
                Date = category.Date
            };

            return View(paymentViewModel);
        }

        [HttpPost]
        public IActionResult ConfirmPayment(PaymentViewModel model)
        {
            var category = _db.Categories.Find(model.Id);
            var user = _db.Users.Find(model.UserId);

            var payment = new Payment
            {
                CategoryId = model.Id,
                UserId = model.UserId,
                PlaceName = model.PlaceName,
                Location = model.Location,
                Date = model.Date,
                Name = model.UserName,
                Number = model.UserPhoneNumber
            };

            if (category == null || user == null || category.Capacity <= 0)
            {
                TempData["OutOfCapacity"] = true;
                return RedirectToAction("Details", new { id = model.Id });
            }

           

            _db.Payments.Add(payment);
            category.Capacity--; // Decrease capacity by 1
            _db.SaveChanges();

            TempData["BookingSuccess"] = true;
            return RedirectToAction("Index");
        }


    }
}
