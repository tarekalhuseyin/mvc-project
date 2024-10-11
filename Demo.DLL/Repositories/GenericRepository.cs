﻿using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Demo.DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MvcAppG01DbContext _dbContext;

        public GenericRepository(MvcAppG01DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T item)
        {
           await _dbContext.AddAsync(item);
          
        }

        public void Delete(T item)
        {
           _dbContext.Remove(item);
           
        }

        public async Task <IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _dbContext.Employees.Include(E=>E.Department).ToListAsync();
            }
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        =>
           await _dbContext.Set<T>().FindAsync(id);
        

        public void Update(T item)
        {
            _dbContext.Update(item);
          
        }
    }
}
