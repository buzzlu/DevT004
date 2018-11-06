﻿using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
   public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid guid);
        Task<List<T>> ListAllAsync();
        Task<List<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
