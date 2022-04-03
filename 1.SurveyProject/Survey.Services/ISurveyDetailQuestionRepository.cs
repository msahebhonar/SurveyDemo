using System;

namespace Survey.Services
{
    public interface ISurveyDetailQuestionRepository
    {
        int GetNumberOfQuestionsInSurvey(Guid surveyDetailId);
    }
}