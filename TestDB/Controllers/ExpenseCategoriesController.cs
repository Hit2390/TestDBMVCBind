using System;
using System.Web.Mvc;
using FinanceReport.Models.POCO;
using FinanceReport.Models.Repository;

namespace FinanceReport.Controllers
{
    public class ExpenseCategoriesController : Controller
    {
        private readonly ExpenseCategoryRepository _expenseCategoryRepository = new ExpenseCategoryRepository();

        public ActionResult Index()
        {
            try
            {
                var categories = _expenseCategoryRepository.GetAllExpenseCategories();
                return View(categories);
            }
            catch
            {
                // Log exception and return an error view
                ViewBag.ErrorMessage = "An error occurred while retrieving expense categories.";
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ExpenseCategory category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _expenseCategoryRepository.AddExpenseCategory(category);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                // Log exception and return an error view
                ViewBag.ErrorMessage = "An error occurred while creating the expense category.";
                return View("Error");
            }

            return View(category);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var category = _expenseCategoryRepository.GetExpenseCategoryById(id);
                if (category == null)
                {
                    return HttpNotFound();
                }

                return View(category);
            }
            catch
            {
                // Log exception and return an error view
                ViewBag.ErrorMessage = "An error occurred while retrieving the expense category.";
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(ExpenseCategory category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _expenseCategoryRepository.UpdateExpenseCategory(category);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                // Log exception and return an error view
                ViewBag.ErrorMessage = "An error occurred while updating the expense category.";
                return View("Error");
            }

            return View(category);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var category = _expenseCategoryRepository.GetExpenseCategoryById(id);
                if (category == null)
                {
                    return HttpNotFound();
                }

                return View(category);
            }
            catch
            {
                // Log exception and return an error view
                ViewBag.ErrorMessage = "An error occurred while retrieving the expense category.";
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _expenseCategoryRepository.DeleteExpenseCategory(id);
                return RedirectToAction("Index");
            }
            catch
            {
                // Log exception and return an error view
                ViewBag.ErrorMessage = "An error occurred while deleting the expense category.";
                return View("Error");
            }
        }
    }
}
