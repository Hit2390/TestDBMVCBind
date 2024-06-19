using System.Web.Mvc;
using FinanceReport.Models.POCO;
using FinanceReport.Models.Repository;

namespace FinanceReport.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ExpenseRepository _expenseRepository = new ExpenseRepository();
        private readonly ExpenseCategoryRepository _expenseCategoryRepository = new ExpenseCategoryRepository();

        public ActionResult Index()
        {
            try
            {
                var expenses = _expenseRepository.GetAllExpenses();
                return View(expenses);
            }
            catch
            {
                // Log exception and return an error view
                ViewBag.ErrorMessage = "An error occurred while retrieving expense.";
                return View("Error");
            }
           
        }

        public ActionResult Create()
        {
            ViewBag.ExpenseCategories = new SelectList(_expenseCategoryRepository.GetAllExpenseCategories(), "ExpenseCategoryId", "ExpenseCategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _expenseRepository.AddExpense(expense);
                return RedirectToAction("Index");
            }

            ViewBag.ExpenseCategories = new SelectList(_expenseCategoryRepository.GetAllExpenseCategories(), "ExpenseCategoryId", "ExpenseCategoryName");
            return View(expense);
        }

        public ActionResult Edit(int id)
        {
            var expense = _expenseRepository.GetExpenseById(id);
            if (expense == null)
            {
                return HttpNotFound();
            }

            ViewBag.ExpenseCategories = new SelectList(_expenseCategoryRepository.GetAllExpenseCategories(), "ExpenseCategoryId", "ExpenseCategoryName", expense.ExpenseCategoryId);
            return View(expense);
        }

        [HttpPost]
        public ActionResult Edit(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _expenseRepository.UpdateExpense(expense);
                return RedirectToAction("Index");
            }

            ViewBag.ExpenseCategories = new SelectList(_expenseCategoryRepository.GetAllExpenseCategories(), "ExpenseCategoryId", "ExpenseCategoryName", expense.ExpenseCategoryId);
            return View(expense);
        }

        public ActionResult Delete(int id)
        {
            var expense = _expenseRepository.GetExpenseById(id);
            if (expense == null)
            {
                return HttpNotFound();
            }

            return View(expense);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _expenseRepository.DeleteExpense(id);
            return RedirectToAction("Index");
        }
    }
}

