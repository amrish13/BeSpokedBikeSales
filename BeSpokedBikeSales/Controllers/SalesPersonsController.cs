using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikeSales.Data;
using BeSpokedBikeSales.Models;
using BeSpokedBikeSales.Interface;

namespace BeSpokedBikeSales.Controllers
{
    public class SalesPersonsController : Controller
    {
        private readonly ISalesPersonService _salesPersonService;

        public SalesPersonsController(ISalesPersonService salesPersonService)
        {
            _salesPersonService = salesPersonService;
        }

        // GET: SalesPersons
        public async Task<IActionResult> Index()
        {
            return View(_salesPersonService.GetListOfSalesPeople());
        }

        // GET: SalesPersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = _salesPersonService.GetSalesPersonById((int)id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            return View(salesPerson);
        }

        // GET: SalesPersons/Create
        public IActionResult Create()
        {
            return View(new SalesPerson());
        }

        // POST: SalesPersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesPersonId,FirstName,LastName,Address,Phone,StartDate,TerminationDate,Manager")] SalesPerson salesPerson)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ = _salesPersonService.CreateSalesPerson(salesPerson);
                    return RedirectToAction(nameof(Index));
                }
                return View(salesPerson);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(salesPerson);
            }
        }

        // GET: SalesPersons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = _salesPersonService.GetSalesPersonById((int)id);
            if (salesPerson == null)
            {
                return NotFound();
            }
            return View(salesPerson);
        }

        // POST: SalesPersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesPersonId,FirstName,LastName,Address,Phone,StartDate,TerminationDate,Manager")] SalesPerson salesPerson)
        {
            if (id != salesPerson.SalesPersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _salesPersonService.UpdateSalesPerson(salesPerson);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (salesPerson != null && !SalesPersonExists(salesPerson.SalesPersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(salesPerson);
        }

        // GET: SalesPersons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = _salesPersonService.GetSalesPersonById((int)id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            return View(salesPerson);
        }

        // POST: SalesPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesPerson = _salesPersonService.GetSalesPersonById (id);
            if (salesPerson != null)
            {
                _ = _salesPersonService.DeleteSalesPerson(salesPerson.SalesPersonId);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SalesPersonExists(int id)
        {
            var salesPerson = _salesPersonService.GetSalesPersonById(id);

            return salesPerson != null;
        }
    }
}
