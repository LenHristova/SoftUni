namespace P01_HospitalDatabase.Data.EntitiesConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Email)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(80);
        }
    }
}
