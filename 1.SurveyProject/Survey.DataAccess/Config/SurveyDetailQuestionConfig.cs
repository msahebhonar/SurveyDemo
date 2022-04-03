using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Survey.Entities.Survey;

namespace Survey.DataAccess.Config
{
    public class SurveyDetailQuestionConfig : IEntityTypeConfiguration<SurveyDetailQuestion>
    {
        public void Configure(EntityTypeBuilder<SurveyDetailQuestion> builder)
        {
            builder
                .HasKey(x => new { x.SurveyDetailId, x.QuestionBankId });
        }
    }
}
