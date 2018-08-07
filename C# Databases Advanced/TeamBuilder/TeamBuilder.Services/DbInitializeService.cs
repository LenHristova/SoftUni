namespace TeamBuilder.Services
{
    using Contracts;
	using Data;
	using Microsoft.EntityFrameworkCore;

    public class DbInitializeService : IDbInitializeService
    {
        private readonly TeamBuilderContext context;

        public DbInitializeService(TeamBuilderContext context)
        {
            this.context = context;
        }

        public void Initialize()
        {
           // this.context.Database.EnsureDeleted();
            this.context.Database.Migrate();
        }
    }
}
