using System;
using Web.Models.Models;

namespace Web.DataAccess.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category obj);
    
}
