using BeSpokedBikeSales.Data;
using BeSpokedBikeSales.Interface;
using BeSpokedBikeSales.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BeSpokedBikeSales.Services
{
    public class ReportService : IReportService
    {
        private readonly BeSpokedBikeSalesContext _context;
        public ReportService(BeSpokedBikeSalesContext context)
        {
            _context = context;
        }
        public IEnumerable<QuaterlyCommissionReport> GetQuarterlySalesCommissionReport()
        {
            try
            {

                return _context.QuaterlyCommissionReport.FromSqlRaw(@"SELECT
                    p.firstName, p.lastName,
                    DATEPART(YEAR, SalesDate) AS Year,
                    DATEPART(QUARTER, SalesDate) AS Quarter,
                    SUM(SaleCommission) AS TotalCommission,
                    Count(SaleId) AS TotalSales
                FROM Sale s
                Inner JOIN SalesPerson p ON s.SalesPersonId = p.SalesPersonId
                GROUP BY
                    p.firstName,
                    p.lastName,
                    DATEPART(YEAR, SalesDate),
                    DATEPART(QUARTER, SalesDate)
                ORDER BY
                    Year,
                    Quarter;").AsEnumerable();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
