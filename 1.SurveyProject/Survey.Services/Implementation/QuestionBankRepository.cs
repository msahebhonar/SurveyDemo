using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Survey.DataAccess;
using Survey.Entities.Question;

namespace Survey.Services.Implementation
{
    public class QuestionBankRepository : IQuestionBankRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionBankRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuestionBank>> GetQuestionsAsync(Guid surveyDetailId)
        {
            return await _context.SurveyDetailQuestions
                .Where(x => x.SurveyDetailId == surveyDetailId)
                .Include(x => x.QuestionBank)
                .ThenInclude(x => x.Responses)
                .Select(x => x.QuestionBank)
                .Where(x => x.IsActive)
                .ToListAsync();
        }
    }
}