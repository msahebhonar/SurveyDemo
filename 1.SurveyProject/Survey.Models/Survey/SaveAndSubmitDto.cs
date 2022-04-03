using System;
using System.Collections.Generic;

namespace Survey.Models.Survey
{
    public class SaveAndSubmitDto
    {
        public string UserAccountId { get; set; }

        public string SurveyDetailId { get; set; }

        public IEnumerable<RespondentAnswerDto> RespondentAnswerDto { get; set; }
    }
}