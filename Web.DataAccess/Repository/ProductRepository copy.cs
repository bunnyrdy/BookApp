using System;
using System.Linq.Expressions;
using Web.DataAccess.Data;
using Web.DataAccess.Repository.IRepository;
using Web.Models.Models;

namespace Web.DataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private ApplicationDbContext _db;
    public ProductRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

     

    public void Update(Product obj)
    {
        _db.Products.Update(obj);
    }
}
