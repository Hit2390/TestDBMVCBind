using FinanceReport.Models.Repository;
using System.Web.Mvc;

namespace FinanceReport.Controllers
{
    public class ReportController : Controller
    {
        private readonly ReportRepository _reportRepository = new ReportRepository();

        // GET: Report
        public ActionResult Index()
        {
            var reports = _reportRepository.GetAllReports();
            return View(reports);
        }
    }
}
