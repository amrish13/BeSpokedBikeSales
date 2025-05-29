using BeSpokedBikeSales.Interface;
using BeSpokedBikeSales.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikeSales.Controllers
{
    public class DiscountsController : Controller
    {
        private IDiscountService _service;
        private IProductsService _productService;

        public DiscountsController(IDiscountService service, IProductsService productService)
        {
            _service = service;
            _productService = productService;
        }

        // GET: Discounts
        public async Task<IActionResult> Index()
        {
            return View(_service.GetListOfDiscounts());
        }

        // GET: Discounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = _service.GetDiscountById((int)id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // GET: Discounts/Create
        public IActionResult Create()
        {
            ViewBag.ProductsList = _productService.GetProductSelectList();
            return View(new Discount());
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscountId,ProductId,BeginDate,EndDate,DiscountPercentage")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                _service.CreateDiscount(discount);
                return RedirectToAction(nameof(Index));
            }
            return View(discount);
        }

        // GET: Discounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = _service.GetDiscountById((int)id);
            if (discount == null)
            {
                return NotFound();
            }
            ViewBag.ProductsList = _productService.GetProductSelectList();
            return View(discount);
        }

        // POST: Discounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscountId,ProductId,BeginDate,EndDate,DiscountPercentage")] Discount discount)
        {
            if (id != discount.DiscountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateDiscount(discount);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_service.GetDiscountById(id) == null)
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
            ViewBag.ProductsList = new SelectList(_productService.GetProductSelectList(), "ProductId", "Name", discount.ProductId);
            return View(discount);
        }

        // GET: Discounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = _service.GetDiscountById((int)id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // POST: Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            _service.DeleteDiscount(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
