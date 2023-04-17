using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public ProductsController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await mvcDemoDbContext.Products.ToListAsync();
            return View(products);
        }

        [HttpGet]       
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel addProductsRequest)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                PriceListName = addProductsRequest.PriceListName,
                MaterialCode = addProductsRequest.MaterialCode,
                MaterialName = addProductsRequest.MaterialName,
                MaterialGroupCode = addProductsRequest.MaterialGroupCode,
                BrandCode = addProductsRequest.BrandCode,
                Model = addProductsRequest.Model,
                PriceList = addProductsRequest.PriceList,
                Stock = addProductsRequest.Stock
            };

            await mvcDemoDbContext.Products.AddAsync(product);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var product = await mvcDemoDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if(product != null)
            {
                var viewModel = new UpdateProductViewModel()
                {
                    Id = product.Id,
                    PriceListName = product.PriceListName,
                    MaterialCode = product.MaterialCode,
                    MaterialName = product.MaterialName,
                    MaterialGroupCode = product.MaterialGroupCode,
                    BrandCode = product.BrandCode,
                    Model = product.Model,
                    PriceList = product.PriceList,
                    Stock = product.Stock
                };
                return await Task.Run(() => View("View", viewModel));
            }

            

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateProductViewModel viewModel)
        {
            {
                var product = await mvcDemoDbContext.Products.FindAsync(viewModel.Id);

                if (product != null)
                {
                    product.PriceListName = viewModel.PriceListName;
                    product.MaterialCode = viewModel.MaterialCode;
                    product.MaterialName = viewModel.MaterialName;
                    product.MaterialGroupCode = viewModel.MaterialGroupCode;
                    product.BrandCode = viewModel.BrandCode;
                    product.Model = viewModel.Model;
                    product.PriceList = viewModel.PriceList;
                    product.Stock = viewModel.Stock;

                    await mvcDemoDbContext.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateProductViewModel viewModel)
        {
            var product = await mvcDemoDbContext.Products.FindAsync(viewModel.Id);
            if(product != null)
            {
                mvcDemoDbContext.Products.Remove(product);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
