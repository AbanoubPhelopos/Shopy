﻿using Microsoft.AspNetCore.Mvc;
using Shopy.DataAccess.Data;
using Shopy.Entities.Models;
using Shopy.Reposatory;

namespace Shopy.Controllers;

public class CategoryController : Controller
{
    private IUnitOfWork _unitOfWork;
    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        var Categories = _unitOfWork.Category.GetAll();
        return View(Categories);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(category);
            _unitOfWork.Complite();
            TempData["success"] = "The category has Created successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
    
    [HttpGet]
    public IActionResult Edit(int? Id)
    {
        if (Id == null || Id == 0)
        {
            return NotFound();
        }

        var category = _unitOfWork.Category.GetFirstOrDesault(x => x.Id == Id);
        return View(category);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Complite();
            TempData["Info"] = "The category has Updated successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
    [HttpGet]
    public IActionResult Delete(int? Id)
    {
        if (Id == null || Id == 0)
        {
            return NotFound();
        }

        Category? category = _unitOfWork.Category.GetFirstOrDesault(x => x.Id == Id);
        return View(category);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? Id)
    {
        Category? category = _unitOfWork.Category.GetFirstOrDesault(x => x.Id == Id);
        if (category == null)
        {
            return NotFound();
        }
        _unitOfWork.Category.Remove(category);
        _unitOfWork.Complite();
        TempData["error"] = "The category has deleted successfully";
        return RedirectToAction("Index");
    }
}