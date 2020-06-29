using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;
        private DbSet<TEntity> entities;

        public GenericRepository(DataContext context)
        {
            _context = context;
            entities = context.Set<TEntity>();
        }

        public async Task<TEntity> Create(TEntity obj)
        {
            obj.LastUpdate = DateTime.Now;
            var entityEntry = await _context.AddAsync(obj);
            await Save();

            return entityEntry.Entity;
        }

        public async Task<TOtherEntity> Create<TOtherEntity>(TOtherEntity obj) where TOtherEntity : BaseEntity
        {
            obj.LastUpdate = DateTime.Now;
            var entityEntry = await _context.Set<TOtherEntity>().AddAsync(obj);
            await Save();
            return entityEntry.Entity;
        }

        public async Task<TEntity> Delete(TEntity obj)
        {
            var entityEntry = _context.Remove(obj);
            await Save();

            return entityEntry.Entity;
        }

        public async Task<List<TEntity>> GetAll()
        {
            var objList = await entities.ToListAsync();
            return objList;
        }

        public async Task<TEntity> Get(int id)
        {
            var obj = await entities.FirstOrDefaultAsync(x => x.ID == id);
            return obj;
        }

        public async Task<T2> Get<T2>(int id) where T2 : BaseEntity
        {
            var entity = await _context.Set<T2>().FirstOrDefaultAsync(x => x.ID == id);
            return entity;
        }

        public async Task<TEntityOther> Get<TEntityOther>(Expression<Func<TEntityOther, bool>> predicate) where TEntityOther : BaseEntity
        {
            var entiti = await _context
                .Set<TEntityOther>()
                .FirstOrDefaultAsync(predicate);
            return entiti;
        }

        public async Task<List<TEntityOther>> GetAll<TEntityOther>(Expression<Func<TEntityOther, bool>> predicate) where TEntityOther : BaseEntity
        {
            var entitiesList = await _context
                .Set<TEntityOther>()
                .Where(predicate)
                .ToListAsync();

            return entitiesList;
        }

        public async Task<bool> IsExist(Expression<Func<TEntity, bool>> predicate)
        {
            if (await entities.FirstOrDefaultAsync(predicate) != null)
                return true;
            else return false;
        }

        public async Task<TEntity> Update(TEntity obj)
        {
            obj.LastUpdate = DateTime.Now;
            var entityEntry = entities.Update(obj);
            await Save();

            return entityEntry.Entity;
        }

        private async Task Save()
        {
            if (await _context.SaveChangesAsync() == 0)
                throw new Exception("Nie zapisano danych w bazie danych");
        }

        public async Task<bool> IsExist<TEntityOther>(Expression<Func<TEntityOther, bool>> predicate) where TEntityOther : BaseEntity
        {
            if (await _context.Set<TEntityOther>().FirstOrDefaultAsync(predicate) != null)
                return true;
            else return false;
        }
    }
}
