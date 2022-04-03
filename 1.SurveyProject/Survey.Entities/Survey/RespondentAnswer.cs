using Survey.Entities.Question;

namespace Survey.Entities.Survey
{
    public class RespondentAnswer:IAuditable
    {
        public int RespondentAnswerId { get; set; }

        public int RespondentId { get; set; }

        public int QuestionBankId { get; set; }

        public string Answer { get; set; }

        public Respondent Respondent { get; set; }

        public QuestionBank QuestionBank { get; set; }
    }
}