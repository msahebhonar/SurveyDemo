using System;
using Survey.Entities.User;

namespace Survey.Entities.Survey
{
    public class Respondent
    {
        public int RespondentId { get; set; }

        public Guid SurveyDetailId { get; set; }

        public Guid UserAccountId { get; set; }

        public DateTime SendTime { get; set; }

        public DateTime? SubmitTime { get; set; }

        public SurveyDetail SurveyDetail { get; set; }

        public UserAccount UserAccount { get; set; }
    }
}
