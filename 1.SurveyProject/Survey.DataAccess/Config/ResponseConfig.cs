using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Survey.Entities.Question;

namespace Survey.DataAccess.Config
{
    public class ResponseConfig : IEntityTypeConfiguration<Response>
    {
        public void Configure(EntityTypeBuilder<Response> builder)
        {
            builder
                .Property(x => x.Text)
                .HasMaxLength(500);
        }
    }
}
