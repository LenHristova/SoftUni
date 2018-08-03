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
    using Models.Enums;

    public class AlbumService : IAlbumService
    {
        private readonly PhotoShareContext context;
        private readonly IMapper mapper;

        public AlbumService(PhotoShareContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TModel ById<TModel>(int id)
            => By<TModel>(a => a.Id == id).SingleOrDefault();

        public TModel ByName<TModel>(string name)
            => By<TModel>(a => a.Name == name).SingleOrDefault();

        public bool Exists(int id)
            => ById<Album>(id) != null;

        public bool Exists(string name)
            => ByName<Album>(name) != null;

        public Album Create(int userId, string albumTitle, Color bgColor, IEnumerable<int> tagsIds)
        {
            var album = new Album
            {
                Name = albumTitle,
                BackgroundColor = bgColor
            };

            this.context.Albums.Add(album);
            this.context.SaveChanges();

            var albumrole = new AlbumRole
            {
                UserId = userId,
                Album = album
            };

            this.context.AlbumRoles.Add(albumrole);
            this.context.SaveChanges();

            var albumTags = tagsIds
                .Select(tId => new AlbumTag
                {
                    Album = album,
                    TagId = tId
                })
                .ToArray();

            this.context.AlbumTags.AddRange(albumTags);
            this.context.SaveChanges();

            return album;
        }

        private IEnumerable<TModel> By<TModel>(Func<Album, bool> predicate)
            => this.context.Albums
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>(mapper.ConfigurationProvider);
    }
}
