namespace TeamBuilder.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class TeamEventConfig : IEntityTypeConfiguration<TeamEvent>
    {
        public void Configure(EntityTypeBuilder<TeamEvent> builder)
        {
            builder
                .HasKey(te => new {te.TeamId, te.EventId});

            builder
                .HasOne(te => te.Team)
                .WithMany(t => t.Events)
                .HasForeignKey(te => te.TeamId);

            builder
                .HasOne(te => te.Event)
                .WithMany(e => e.ParticipatingTeams)
                .HasForeignKey(te => te.EventId);
        }
    }
}
