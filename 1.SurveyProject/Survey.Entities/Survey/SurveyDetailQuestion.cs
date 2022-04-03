using System;
using Survey.Entities.Question;

namespace Survey.Entities.Survey
{
    public class SurveyDetailQuestion
    {
        public Guid SurveyDetailId { get; set; }

        public int QuestionBankId { get; set; }

        public SurveyDetail SurveyDetail { get; set; }

        public QuestionBank QuestionBank { get; set; }
    }
}
