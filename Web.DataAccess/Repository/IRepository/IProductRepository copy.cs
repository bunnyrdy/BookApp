using System;
using Web.Models.Models;

namespace Web.DataAccess.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product obj);
    
}
