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
    public class ProductController : Controller    
    {
        // GET: CategoryController

        private readonly  IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
            
        }
        

        public ActionResult ProductIndex()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
         public ActionResult Create(Product obj)
         {
            //custom error message
            
            if(ModelState.IsValid){
            _unitOfWork.Product.Add(obj);
            _unitOfWork.Save();
           // TempData["success"] = "Category created successfully";
            return RedirectToAction("ProductIndex");
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
            Product? productFromdb = _unitOfWork.Product.Get(u => u.Product_Id == id);
          //third way of retrival is using where


           if(productFromdb == null){
            return NotFound();
           }
           return View(productFromdb);
        }
        [HttpPost]
         public ActionResult Edit(Product obj)
         {
            
            //if(ModelState.IsValid)
            //{
            
            _unitOfWork.Product.Update(obj);
           _unitOfWork.Save();
            TempData["success"] = "Category edited successfully";
            return RedirectToAction("ProductIndex");
            // }
            //  return View();
            


    }

    public ActionResult Delete(int id)
        {
           if(id==null || id==0){
                return NotFound();
           }

            
            Product? productFromdb = _unitOfWork.Product.Get(u => u.Product_Id==id);
        

           
           if(productFromdb == null){
            return NotFound();
           }
           return View(productFromdb);
        }
        [HttpPost,ActionName("Delete")]
         public ActionResult DeletePost(int id)
         {
            Product? obj = _unitOfWork.Product.Get(u => u.Product_Id==id);
            if (obj == null){
                return NotFound();
            }
            
            
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("ProductIndex");

    }
}
}

