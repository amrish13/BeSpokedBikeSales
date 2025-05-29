using BeSpokedBikeSales.Models;

namespace BeSpokedBikeSales.Interface
{
    public interface IReportService
    {
        public IEnumerable<QuaterlyCommissionReport> GetQuarterlySalesCommissionReport();
    }
}
