using BeSpokedBikeSales.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BeSpokedBikeSales.Controllers
{
    public class ReportController : Controller
    {
        IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        public IActionResult Index()
        {
            var data = _reportService.GetQuarterlySalesCommissionReport();
            return View(data);
        }
    }
}
