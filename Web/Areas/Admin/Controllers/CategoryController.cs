using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Web.DataAccess.Data;
using Web.DataAccess.Repository.IRepository;
using Web.Models;
using Web.Models.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller    
    {
        // GET: CategoryController

        private readonly  IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
            
        }
        

        public ActionResult Index1()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public ActionResult Create(){
            return View();
        }
        [HttpPost]
         public ActionResult Create(Category obj)
         {
            //custom error message
            if(obj.Name == obj.DisplayOrder.ToString()){
                ModelState.AddModelError("Name", "Name cannot be equal to DisplayOrder");
            }
            if(ModelState.IsValid){
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index1");
            }else{
                return View();
            }
            
        }

        public ActionResult Edit(int id)
        {
           if(id == null || id==0){
                return NotFound();
           }

            //the first way of retriving
            // Category? categoryFromDb = _db.categories.Find(id);
            //second way of retrival
            Category? categoryFromdb = _unitOfWork.Category.Get(u => u.Category_Id == id);
          //third way of retrival is using where

           
           if(categoryFromdb == null){
            return NotFound();
           }
           return View(categoryFromdb);
        }
        [HttpPost]
         public ActionResult Edit(Category obj)
         {
            
            if(ModelState.IsValid)
            {
            
            _unitOfWork.Category.Update(obj);
           _unitOfWork.Save();
            TempData["success"] = "Category edited successfully";
            return RedirectToAction("Index1");
            }
             return View();
            


    }

    public ActionResult Delete(int id)
        {
           if(id==null || id==0){
                return NotFound();
           }

            
            Category? categoryFromdb = _unitOfWork.Category.Get(u => u.Category_Id==id);
        

           
           if(categoryFromdb == null){
            return NotFound();
           }
           return View(categoryFromdb);
        }
        [HttpPost,ActionName("Delete")]
         public ActionResult DeletePost(int id)
         {
            Category? obj = _unitOfWork.Category.Get(u => u.Category_Id==id);
            if (obj == null){
                return NotFound();
            }
            
            
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index1");

    }
}
}

