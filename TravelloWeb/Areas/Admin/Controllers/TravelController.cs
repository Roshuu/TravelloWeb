using Travello.DataAccess;
using Travello.DataAccess.Repository;
using Travello.DataAccess.Repository.IRepository;
using Travello.Models;
using Travello.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Travello.DataAccess.Repository.IRepository;
using Travello.Models.ViewModels;

namespace TravelloWeb.Controllers;
[Area("Admin")]
public class TravelController : Controller
{
    private readonly IMain _main;
    private readonly IWebHostEnvironment _hostEnvironment;

    public TravelController(IMain main, IWebHostEnvironment hostEnvironment)
    {
        _main = main;
        _hostEnvironment = hostEnvironment;
    }

    public IActionResult Index()
    {

        return View();
    }


    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Upsert(int? id)
    {

        ProductVM productVM = new()
        {
            Travel = new(),
            GuideList = _main.Guide.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name+" "+i.Surname,
                Value = i.Id.ToString()
            }),

        };

        if (id == null || id == 0)
        {

            // ViewBag.CategoryList = CategoryList;
            // ViewData["CoverTypeList"] = CoverTypeList;
            return View(productVM);

        }

        else
        {
            productVM.Travel = _main.Travel.GetFirstOrDefault(u => u.Id == id);
            return View(productVM);
        }
        //var categoryFromDb = _db.categories.Find(id);



    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM obj, IFormFile? file)
    {


        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\travels");
                var extension = Path.GetExtension(file.FileName);

                if (obj.Travel.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, obj.Travel.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }

                obj.Travel.ImageUrl = @"\images\travels\" + fileName + extension;

            }

            if (obj.Travel.Id == 0)
            {
                _main.Travel.Add(obj.Travel);
            }
            else
            {
                _main.Travel.Update(obj.Travel);
            }
            //_unitOfWork.CoverType.Update(obj);
            // _unitOfWork.Product.Add(obj.Product);
            _main.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index");
        }

        return View(obj);
    }







    [HttpGet]
    public IActionResult GetAll()
    {
        var travelList = _main.Travel.GetAll(includeProperties: "Guide");
        return Json(new { data = travelList });
    }


    [HttpDelete]
    public IActionResult Delete(int? id)
    {

        var obj = _main.Travel.GetFirstOrDefault(u => u.Id == id);

        if (obj == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }

        _main.Travel.Remove(obj);
        _main.Save();
        return Json(new { success = true, message = "Delete Sucessful" });
    }
}




