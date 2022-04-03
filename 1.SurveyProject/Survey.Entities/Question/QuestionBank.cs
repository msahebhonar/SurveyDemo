using Survey.Entities.Survey;
using System.Collections.Generic;
using Survey.Common;

namespace Survey.Entities.Question
{
    public class QuestionBank
    {
        public int QuestionBankId { get; set; }

        public string Text { get; set; }

        public QuestionType QuestionType { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Response> Responses { get; set; }

        public ICollection<SurveyDetailQuestion> SurveyDetailQuestions { get; set; }
    }
}

