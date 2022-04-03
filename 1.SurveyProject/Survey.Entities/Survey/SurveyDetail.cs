using System;
using System.Collections.Generic;

namespace Survey.Entities.Survey
{
    public class SurveyDetail
    {
        public Guid SurveyDetailId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<SurveyDetailQuestion> SurveyDetailQuestions { get; set; }
    }
}
