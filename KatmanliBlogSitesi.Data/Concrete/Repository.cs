﻿using KatmanliBlogSitesi.Data.Abstract;
using KatmanliBlogSitesi.Entites;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KatmanliBlogSitesi.Data.Concrete
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        protected DatabaseContext context;
        protected DbSet<T> dbSet; // protected, güvenli olarak demek. veritabanı için boş generic dbset oluşturduk
        public Repository(DatabaseContext _context)// ctor yazıp iki kez tab tuşuna basarsak burası hemen oluşur. constructor oluştururuz.
        {
            context = _context;
            dbSet = context.Set<T>(); // boş dbset i context içindeki ilgili class ın db seti için ayarladık
        }
        public int Add(T entity)
        {
            dbSet.Add(entity);
            return SaveChanges();
        }

        public async Task AddAsync(T entity)
        {
            //await context.AddAsync(entity);
            await dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            context.Remove(entity);
        }

        public T Find(int id)
        {
            return dbSet.Find(id);
        }

        public IQueryable<T> FindAllAsync(Expression<Func<T, bool>> expressions)
        {
            return dbSet.Include(expressions);
        }

        public async Task<T> FindAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expressions)
        {
            return await dbSet.FirstOrDefaultAsync(expressions);
        }

        public T Get(Expression<Func<T, bool>> expressions)
        {
            return dbSet.FirstOrDefault(expressions);
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expressions)
        {
            return dbSet.Where(expressions).ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expressions)
        {
            return await dbSet.Where(expressions).ToListAsync();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            context.Update(entity);
        }
    }
}
