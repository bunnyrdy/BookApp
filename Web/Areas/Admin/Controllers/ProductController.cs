using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Web.DataAccess.Data;
using Web.DataAccess.Repository.IRepository;
using Web.Models;
using Web.Models.Models;
using Web.Models.ViewModel;


namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller    
    {
        // GET: CategoryController

        private readonly  IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
           _unitOfWork = unitOfWork;
           _webHostEnvironment = webHostEnvironment;
            
        }
        
    
        public ActionResult ProductIndex()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
           
            return View(objProductList);
        }
        public ActionResult Upsert(int? id)
        {
             IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem{
                Text = u.Name,
                Value = u.Category_Id.ToString()
            });
            //ViewBag.CategoryList = CategoryList;
            ProductVM productVM = new(){
                CategoryList = CategoryList,
                Product = new Product()
            };
            if (id == null || id == 0){
                //create
                return View(productVM);
            }else{
                //update
                productVM.Product = _unitOfWork.Product.Get(u => u.Product_Id == id);
                return View(productVM);
            }
            
        }
        [HttpPost]
         public ActionResult Upsert(ProductVM obj,IFormFile? file)
         {
            //custom error message
    
            if(ModelState.IsValid){
                string wwwRootPath = _webHostEnvironment.WebRootPath;
            if(file != null){
               string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
               string productPath = Path.Combine(wwwRootPath,@"Images/Product");
               if(!string.IsNullOrEmpty(obj.Product.Imageurl)){
                //delete the od image and update the new image 
                var oldImagePath = Path.Combine(wwwRootPath,obj.Product.Imageurl.TrimStart('\\'));
                if(System.IO.File.Exists(oldImagePath)){
                    System.IO.File.Delete(oldImagePath);
                }
               }
               using (var fileStream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create)){
                file.CopyTo(fileStream);
                
               }
                obj.Product.Imageurl = @"Images\Product\" + fileName;
            }
            if(obj.Product.Product_Id == 0){
                _unitOfWork.Product.Add(obj.Product);
            }
            else{
            _unitOfWork.Product.Update(obj.Product);
            }
            _unitOfWork.Save();
           // TempData["success"] = "Category created successfully";
            return RedirectToAction("ProductIndex");                                                                                
            }else{
                return View();
            }
            
        }
    #region API CALLS   
     [HttpGet]
        public ActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

        public ActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Product_Id == id);
            if(productToBeDeleted == null){
                return Json(new {success = false,message = "Error while deleting"});
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,productToBeDeleted.Imageurl.TrimStart('\\'));
                if(System.IO.File.Exists(oldImagePath)){
                    System.IO.File.Delete(oldImagePath);
                }
                _unitOfWork.Product.Remove(productToBeDeleted);
                _unitOfWork.Save();
                return Json(new {success = true,message = "Deleted successfully"});
            
        }

    #endregion
}
}

