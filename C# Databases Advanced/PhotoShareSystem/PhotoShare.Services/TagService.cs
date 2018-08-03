namespace PhotoShare.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Models;

    public class TagService : ITagService
    {
        private readonly PhotoShareContext context;
        private readonly IMapper mapper;

        public TagService(PhotoShareContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TModel ById<TModel>(int id)
            => By<TModel>(a => a.Id == id).SingleOrDefault();

        public TModel ByName<TModel>(string name)
            => By<TModel>(a => a.Name == name).SingleOrDefault();

        public bool Exists(int id)
            => ById<Tag>(id) != null;

        public bool Exists(string name)
            => ByName<Tag>(name) != null;

        public Tag AddTag(string name)
        {
            var tag = new Tag
            {
                Name = name
            };

            this.context.Tags.Add(tag);
            this.context.SaveChanges();

            return tag;
        }

        private IEnumerable<TModel> By<TModel>(Func<Tag, bool> predicate)
        => this.context.Tags
            .Where(predicate)
            .AsQueryable()
            .ProjectTo<TModel>(mapper.ConfigurationProvider);
    }
}
