namespace BusTickets.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Models;

    public interface IRepository<TEntity> where TEntity : EntityBase

    {
        TDto GetById<TDto>(int id);

        IEnumerable<TDto> List<TDto>();

        IEnumerable<TDto> List<TDto>(Expression<Func<TEntity, bool>> predicate);

        void Add<TDto>(TDto dto);

        void Delete<TDto>(TDto dto);

        void Edit<TDto>(TDto dto);

        TDto Get<TDto>(Expression<Func<TEntity, bool>> predicate);
    }
}
