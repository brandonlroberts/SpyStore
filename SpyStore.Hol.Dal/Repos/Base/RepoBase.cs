﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using SpyStore.Hol.Dal.EfStructures;
using SpyStore.Hol.Dal.Exceptions;
using SpyStore.Hol.Models.Entities.Base;

namespace SpyStore.Hol.Dal.Repos.Base
{
    public abstract class RepoBase<T> : IDisposable, IRepo<T> where T : EntityBase, new()
    {
        private readonly bool _disposeContext;
        protected readonly DbSet<T> Table;
        public StoreContext Context { get; }

        protected RepoBase(StoreContext context)
        {
            Context = context;
            Table = Context.Set<T>();
            _disposeContext = false;
        }

        protected RepoBase(DbContextOptions<StoreContext> options) : this(new StoreContext(options))
        {
            _disposeContext = true;
        }

        public void Dispose()
        {
            if (_disposeContext)
            {
                Context.Dispose();
            }
        }

        public T Find(int? id) => Table.Find(id);

        public T FindAsNoTracking(int id)
            => Table.Where(x => x.Id == id).AsNoTracking().FirstOrDefault();

        public T FindIgnoreQueryFilters(int id)
            => Table.IgnoreQueryFilters().FirstOrDefault(x => x.Id == id);

        public virtual IEnumerable<T> GetAll() => Table;

        public virtual IEnumerable<T> GetAll(Expression<Func<T, object>> orderBy)
            => Table.OrderBy(orderBy);

        public IEnumerable<T> GetRange(IQueryable<T> query, int skip, int take)
            => query.Skip(skip).Take(take);

        public virtual int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }
        public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }
        public virtual int Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }
        public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }
        public virtual int Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }
        public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex) //A concurrency error occurred
            {
                throw new SpyStoreConcurrencyException("A concurrency error happened.", ex);
            }
            catch (RetryLimitExceededException ex) //DbResiliency retry limit exceeded
            {
                throw new SpyStoreRetryLimitExceededException("There is a problem with you connection.", ex);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException)
                {
                    if (sqlException.Message
                        .Contains("FOREIGN KEY constraint", StringComparison.OrdinalIgnoreCase))
                    {
                        if (sqlException.Message
                            .Contains("table \"Store.Products\", column 'Id'", StringComparison.OrdinalIgnoreCase))
                        {
                            throw new SpyStoreInvalidProductException($"Invalid Product Id\r\n{ex.Message}", ex);
                        }
                        if (sqlException.Message
                            .Contains("table \"Store.Customers\", column 'Id'", StringComparison.OrdinalIgnoreCase))
                        {
                            throw new SpyStoreInvalidCustomerException($"Invalid Customer Id\r\n{ex.Message}", ex);
                        }
                    }
                }
                throw new SpyStoreException("An error occurred updating the database", ex);
            }
            catch (Exception ex)
            {
                throw new SpyStoreException("An error occurred updating the database", ex);
            }
        }
    }
}
