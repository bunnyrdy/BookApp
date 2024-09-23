using System;
using System.Linq.Expressions;
using Web.DataAccess.Data;
using Web.DataAccess.Repository.IRepository;
using Web.Models.Models;

namespace Web.DataAccess.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private ApplicationDbContext _db;
    public CategoryRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

     

    public void Update(Category obj)
    {
        _db.categories.Update(obj);
    }
}
