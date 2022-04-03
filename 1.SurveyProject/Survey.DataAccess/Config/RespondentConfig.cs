using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Survey.Entities.Survey;

namespace Survey.DataAccess.Config
{
    public class RespondentConfig : IEntityTypeConfiguration<Respondent>
    {
        public void Configure(EntityTypeBuilder<Respondent> builder)
        {
            builder
                .HasIndex(x => new { x.UserAccountId, x.SurveyDetailId })
                .IsUnique();

            builder
                .HasOne(x => x.SurveyDetail)
                .WithMany()
                .HasForeignKey(x => x.SurveyDetailId);

            builder
                .HasOne(x => x.UserAccount)
                .WithMany()
                .HasForeignKey(x => x.UserAccountId);
        }
    }
}
    