using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelFirst.Models;

namespace ModelFirst.Configurations
{
    public class PersonCarsTypeConfiguration : IEntityTypeConfiguration<PersonCars>
    {
        public void Configure(EntityTypeBuilder<PersonCars> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.Person)
                .WithMany(X => X.PersonCarss)
                .HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
