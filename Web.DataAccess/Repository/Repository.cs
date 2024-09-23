using System;
using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;

namespace Web.DataAccess.Repository.IRepository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;
    public Repository(ApplicationDbContext db){
        _db = db;
       this.dbSet = _db.Set<T>();
      // _db.categories == dbSet;
      
    }


    public void Add(T entity)
    {
       dbSet.Add(entity);
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
       IQueryable<T> query = dbSet;
       query = query.Where (filter);
       return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll()
    {
         IQueryable<T> query = dbSet;
         return query.ToList();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
    }
}
