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
public class GuideController : Controller
{
    private readonly IMain _main;
    private readonly IWebHostEnvironment _hostEnvironment;

    public GuideController(IMain main, IWebHostEnvironment hostEnvironment)
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

        Guide guide = new();

       

        if (id == null || id == 0)
        {

            // ViewBag.CategoryList = CategoryList;
            // ViewData["CoverTypeList"] = CoverTypeList;
            return View(guide);

        }

        else
        {
            guide = _main.Guide.GetFirstOrDefault(u => u.Id == id);
            return View(guide);
        }
        //var categoryFromDb = _db.categories.Find(id);



    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Guide obj, IFormFile? file)
    {

        var errors = ModelState.Values.SelectMany(v => v.Errors);
        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\guides");
                var extension = Path.GetExtension(file.FileName);

                if (obj.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }

                obj.ImageUrl = @"\images\guides\" + fileName + extension;

            }

            if (obj.Id == 0)
            {
                _main.Guide.Add(obj);
            }
            else
            {
                _main.Guide.Update(obj);
            }
            //_unitOfWork.CoverType.Update(obj);
            // _unitOfWork.Product.Add(obj.Product);
            _main.Save();
            TempData["success"] = "Guide created successfully";
            return RedirectToAction("Index");
        }

        return View(obj);
    }







    [HttpGet]
    public IActionResult GetAll()
    {
        var guidetList = _main.Guide.GetAll();
        return Json(new { data = guidetList });
    }


    [HttpDelete]
    public IActionResult Delete(int? id)
    {

        var obj = _main.Guide.GetFirstOrDefault(u => u.Id == id);

        if (obj == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }

        _main.Guide.Remove(obj);
        _main.Save();
        return Json(new { success = true, message = "Delete Sucessful" });
    }
}




