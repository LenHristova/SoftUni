using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            //builder.Property(g => g.Result)
            //    .HasConversion(
            //        v => v.ToString(),
            //        v => (GameResult) Enum.Parse(typeof(GameResult), v));


            builder.HasOne(g => g.HomeTeam)
                .WithMany(ht => ht.HomeGames)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.AwayTeam)
                .WithMany(at => at.AwayGames)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
