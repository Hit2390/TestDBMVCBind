using System.Web.Mvc;
using FinanceReport.Models.POCO;
using FinanceReport.Models.Repository;

namespace FinanceReport.Controllers
{
    public class IncomesController : Controller
    {
        private readonly IncomeRepository _incomeRepository = new IncomeRepository();

        public ActionResult Index()
        {
            var incomes = _incomeRepository.GetAllIncomes();
            return View(incomes);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Income income)
        {
            if (ModelState.IsValid)
            {
                _incomeRepository.AddIncome(income);
                return RedirectToAction("Index");
            }
            return View(income);
        }

        public ActionResult Edit(int id)
        {
            var income = _incomeRepository.GetIncomeById(id);
            if (income == null)
            {
                return HttpNotFound();
            }
            return View(income);
        }

        [HttpPost]
        public ActionResult Edit(Income income)
        {
            if (ModelState.IsValid)
            {
                _incomeRepository.UpdateIncome(income);
                return RedirectToAction("Index");
            }
            return View(income);
        }

        public ActionResult Delete(int id)
        {
            var income = _incomeRepository.GetIncomeById(id);
            if (income == null)
            {
                return HttpNotFound();
            }
            return View(income);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _incomeRepository.DeleteIncome(id);
            return RedirectToAction("Index");
        }
    }
}
