namespace PetClinic.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class PetClinicContext : DbContext
    {
        public PetClinicContext() { }

        public PetClinicContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalAid> AnimalAids { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<ProcedureAnimalAid> ProceduresAnimalAids { get; set; }
        public DbSet<Vet> Vets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Vet>(entity =>
            {
                entity
                    .HasIndex(x => x.PhoneNumber)
                    .IsUnique();
            });


            builder.Entity<AnimalAid>(entity =>
            {
                entity
                    .HasIndex(x => x.Name)
                    .IsUnique();
            });

            builder.Entity<ProcedureAnimalAid>(entity =>
            {
                entity.HasKey(x => new
                {
                    x.AnimalAidId, x.ProcedureId
                });


                entity
                    .HasOne(x => x.Procedure)
                    .WithMany(y => y.ProcedureAnimalAids)
                    .HasForeignKey(x => x.ProcedureId);

                entity
                    .HasOne(x => x.AnimalAid)
                    .WithMany(y => y.AnimalAidProcedures)
                    .HasForeignKey(x => x.AnimalAidId);
            });

            builder.Entity<Animal>(entity =>
            {
                entity
                    .HasOne(x => x.Passport)
                    .WithOne(y => y.Animal)
                    .HasForeignKey<Animal>(x => x.PassportSerialNumber);
            });
        }
    }
}
