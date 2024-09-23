using System;
using System.Linq.Expressions;

namespace Web.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork{
        ICategoryRepository Category{get;}
        IProductRepository Product{get;}
        void Save();
    }
}