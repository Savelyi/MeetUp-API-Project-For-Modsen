using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
       
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(e => e.Place)
                 .IsRequired();
            builder.Property(e => e.Time)
                .IsRequired();
            builder.Property(e=>e.OrganizerId)
                .IsRequired();
            builder.HasOne(e => e.Organizer)
                .WithMany(u => u.Events)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Cascade);
            //builder.HasData(
            //    new Event
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "SomeEvent",
            //        OrganizerId = "e4bf3833-5b1f-4e10-bff5-d948e627ac82",
            //        Place = "SomePlace",
            //        Time = DateTime.Parse("3.18.2022 17:00"),
            //    });
        }
    }
}
