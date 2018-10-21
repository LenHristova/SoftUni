namespace SoftUniCopy.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly SoftUniCopyContext db;
        private readonly IMapper mapper;

        public UserService(SoftUniCopyContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<TModel> All<TModel>()
            => this.db.Users
                .ProjectTo<TModel>(mapper.ConfigurationProvider)
                .ToList();

        public Task<TModel> ById<TModel>(string id)
            => this.db.Users
                .Where(u => u.Id == id)
                .ProjectTo<TModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
    }
}
