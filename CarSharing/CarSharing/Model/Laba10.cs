using CarSharing.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarSharing.ViewModel.Laba10;

namespace CarSharing.ViewModel
{
   public class Laba10
    {
        public interface IRepository<T> where T : class
        {
            IEnumerable<T> GetAll();
            T GetById(int id);
            void Add(T entity);
            void Update(T entity);
            void Delete(int id);
        }



    }
}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IEnumerable<T> GetAll() => _dbSet.ToList();
    public T GetById(int id) => _dbSet.Find(id);
    public void Add(T entity) => _dbSet.Add(entity);
    public void Update(T entity) => _dbSet.Update(entity);
    public void Delete(int id)
    {
        var entity = _dbSet.Find(id);
        if (entity != null) _dbSet.Remove(entity);
    }
}

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Order> Orders { get; }
    void Commit();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly EntityFramework _context;
    private IRepository<User> _userRepository;
    private IRepository<Order> _orderRepository;

    public UnitOfWork(EntityFramework context)
    {
        _context = context;
    }

    public IRepository<User> Users => _userRepository ??= new Repository<User>(_context);
    public IRepository<Order> Orders => _orderRepository ??= new Repository<Order>(_context);

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
