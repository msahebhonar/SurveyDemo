using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Survey.Entities.Survey;

namespace Survey.DataAccess.Config
{
    public class SurveyDetailConfig : IEntityTypeConfiguration<SurveyDetail>
    {
        public void Configure(EntityTypeBuilder<SurveyDetail> builder)
        {
            builder
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.Description)
                .HasMaxLength(1000);

            builder
                .HasMany(x => x.SurveyDetailQuestions)
                .WithOne(x => x.SurveyDetail)
                .HasForeignKey(x => x.SurveyDetailId);
        }
    }
}
