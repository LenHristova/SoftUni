namespace BusTickets.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly BusTicketsContext dbContext;
        protected readonly IMapper mapper;

        public Repository(BusTicketsContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public TDto GetById<TDto>(int id)
        {
            var entity = dbContext.Set<TEntity>().Find(id);

            var dto = this.mapper.Map<TDto>(entity);

            return dto;
        }

        public IEnumerable<TDto> List<TDto>()
            => dbContext.Set<TEntity>()
                .ProjectTo<TDto>(this.mapper.ConfigurationProvider)
                .ToList();

        public IEnumerable<TDto> List<TDto>(Expression<Func<TEntity, bool>> predicate)
            => dbContext.Set<TEntity>()
            .Where(predicate)
            .AsQueryable()
            .ProjectTo<TDto>(this.mapper.ConfigurationProvider);

        public void Add<TDto>(TDto dto)
        {
            var entity = this.mapper.Map<TEntity>(dto);

            dbContext.Set<TEntity>().Add(entity);

            dbContext.SaveChanges();
        }

        public void Delete<TDto>(TDto dto)
        {
            var entity = this.mapper.Map<TEntity>(dto);

            dbContext.Set<TEntity>().Remove(entity);
            dbContext.SaveChanges();
        }

        public void Edit<TDto>(TDto dto)
        {
            var entity = this.mapper.Map<TEntity>(dto);
            var logicalDuplicate = dbContext.Set<TEntity>()
                .Local
                .SingleOrDefault(e => e.Id == entity.Id);

            if (logicalDuplicate != null)
            {
                dbContext.Entry(logicalDuplicate).State = EntityState.Detached;
                dbContext.Set<TEntity>().Attach(entity);

            }
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public TDto Get<TDto>(Expression<Func<TEntity, bool>> predicate)
            => dbContext.Set<TEntity>()
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TDto>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();
    }
}
