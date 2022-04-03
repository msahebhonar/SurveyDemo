using System;
using System.Linq;
using Survey.DataAccess;

namespace Survey.Services.Implementation
{
    public class SurveyDetailQuestionRepository:ISurveyDetailQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public SurveyDetailQuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int GetNumberOfQuestionsInSurvey(Guid surveyDetailId)
        {
            return _context.SurveyDetailQuestions.Count(x => x.SurveyDetailId == surveyDetailId);
        }
    }
}