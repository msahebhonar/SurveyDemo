using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Survey.Entities.Survey;

namespace Survey.DataAccess.Config
{
    public class RespondentAnswerConfig : IEntityTypeConfiguration<RespondentAnswer>
    {
        public void Configure(EntityTypeBuilder<RespondentAnswer> builder)
        {
            builder
                .HasOne(x => x.Respondent)
                .WithMany()
                .HasForeignKey(x => x.RespondentId);

            builder
                .HasOne(x => x.QuestionBank)
                .WithMany()
                .HasForeignKey(x => x.QuestionBankId);

            builder
                .Property(x => x.Answer)
                .HasMaxLength(200);
        }
    }
}
