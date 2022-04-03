using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Survey.Entities.Question;

namespace Survey.DataAccess.Config
{
    public class QuestionBankConfig : IEntityTypeConfiguration<QuestionBank>
    {
        public void Configure(EntityTypeBuilder<QuestionBank> builder)
        {
            builder
                .Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .Property(x => x.QuestionType)
                .IsRequired();

            builder
                .HasMany(x => x.Responses)
                .WithOne();

            builder
                .HasMany(x => x.SurveyDetailQuestions)
                .WithOne(x => x.QuestionBank)
                .HasForeignKey(x => x.QuestionBankId);
        }
    }
}
