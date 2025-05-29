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
    public class SalesController : Controller
    {
        private ISalesService _service;
        private IProductsService _productsService;
        private ISalesPersonService _salesPersonService;
        private ICustomerService _customerService;

        public SalesController(ISalesService service
            ,IProductsService productsService
            ,ISalesPersonService salesPersonService
            ,ICustomerService customerService)
        {
            _service = service;
            _productsService = productsService;
            _salesPersonService = salesPersonService;
            _customerService = customerService;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            return View(_service.GetListOfSales());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = _service.GetSaleById((int)id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewBag.CustomersList = _customerService.GetCustomerSelectList();
            ViewBag.ProductsList = _productsService.GetProductSelectList();
            ViewBag.SalesPeopleList = _salesPersonService.GetSalesPeopleSelectList();

            return View(new Sale());
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleId,SalesDate,CustomerId,ProductId,SalesPersonId")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _service.CreateSale(sale);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CustomersList = new SelectList(_customerService.GetCustomerSelectList(), "CustomerId", "FirstName", sale.CustomerId);
            ViewBag.ProductsList = new SelectList(_productsService.GetProductSelectList(), "ProductId", "Name", sale.ProductId);
            ViewBag.SalesPeopleList = new SelectList(_salesPersonService.GetSalesPeopleSelectList(), "SalesPersonId", "FirstName", sale.SalesPersonId);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = _service.GetSaleById((int)id);
            if (sale == null)
            {
                return NotFound();
            }
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleId,SalesDate,CustomerId,ProductId,SalesPersonId")] Sale sale)
        {
            if (id != sale.SaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateSale(sale);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_service.GetSaleById(id) == null)
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
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = _service.GetSaleById((int)id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            _service.DeleteSale(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
