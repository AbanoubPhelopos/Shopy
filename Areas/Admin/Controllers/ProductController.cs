using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shopy.Entities.Models;
using Shopy.Entities.ViewModels;
using Shopy.Reposatory;

namespace Shopy.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult GetTableData()
    {
        var products = _unitOfWork.Product.GetAll(includeWord: "Category");
        return Json(new { data = products });
    }
    [HttpGet]
    public IActionResult Create()
    {
        ProductVM productVm = new ProductVM()
        {
            Product = new Product(),
            CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            })
        };
        return View(productVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProductVM productVM, IFormFile? file)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                // Log the error or inspect it in the debugger
                Console.WriteLine(error.ErrorMessage);
            }
            return View(productVM); // Return the same view to show the errors
        }
        
        if (ModelState.IsValid)
        {
            string RootPath = _webHostEnvironment.WebRootPath;

            if (file != null)
            {
                string filename = Guid.NewGuid().ToString();
                var upload = Path.Combine(RootPath, @"Images\Product");
                var ext = Path.GetExtension(file.FileName);

                using (var filestream = new FileStream(Path.Combine(upload, filename + ext),FileMode.Create)) 
                {
                    file.CopyTo(filestream);
                }

                productVM.Product.Img = @"Images\Product\" + filename + ext;

            }
            
            _unitOfWork.Product.Add(productVM.Product);
            _unitOfWork.Complite();
            TempData["success"] = "The product has Created successfully";
            return RedirectToAction("Index");
        }

        return View(productVM);
    }

    [HttpGet]
    public IActionResult Edit(int? Id)
    {
        if (Id == null || Id == 0)
        {
            return NotFound();
        }
        ProductVM productVm = new ProductVM()
        {
            Product = _unitOfWork.Product.GetFirstOrDesault(x => x.Id == Id),
            CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            })
        };
        return View(productVm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ProductVM productVM, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string RootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString();
                var upload = Path.Combine(RootPath, @"Images\Product");
                var ext = Path.GetExtension(file.FileName);

                if (productVM.Product.Img != null)
                {
                    var oldImg = Path.Combine(RootPath, productVM.Product.Img).TrimStart('\\');
                    if (System.IO.File.Exists(oldImg))
                    {
                        System.IO.File.Delete(oldImg);
                    }
                }
                using (var filestream = new FileStream(Path.Combine(upload, filename + ext),FileMode.Create)) 
                {
                    file.CopyTo(filestream);
                }
                productVM.Product.Img = @"Images\Product" + filename + ext;
            }
            _unitOfWork.Product.Update(productVM.Product);
            _unitOfWork.Complite();
            TempData["Info"] = "The product has Updated successfully";
            return RedirectToAction("Index");
        }
        return View(productVM);
    }
    [HttpGet]
    public IActionResult Delete(int? Id)
    {
        if (Id == null || Id == 0)
        {
            return NotFound();
        }

        Product? product = _unitOfWork.Product.GetFirstOrDesault(x => x.Id == Id);
        return View(product);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? Id)
    {
        Product? product = _unitOfWork.Product.GetFirstOrDesault(x => x.Id == Id);
        if (product == null)
        {
            return NotFound();
        }
        _unitOfWork.Product.Remove(product);
        _unitOfWork.Complite();
        TempData["error"] = "The product has deleted successfully";
        return RedirectToAction("Index");
    }
}