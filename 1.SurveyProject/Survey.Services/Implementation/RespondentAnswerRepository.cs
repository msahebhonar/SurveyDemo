using Microsoft.EntityFrameworkCore;
using Survey.DataAccess;
using Survey.Entities.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Services.Implementation
{
    public class RespondentAnswerRepository : IRespondentAnswerRepository
    {
        private readonly ApplicationDbContext _context;

        public RespondentAnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RespondentAnswer>> GetAllAnswersAsync(int respondentId)
        {
            return await _context.RespondentAnswers
                .Where(x => x.RespondentId == respondentId).ToListAsync();
        }

        public bool SaveAnswersInContext(int respondentId, IEnumerable<RespondentAnswer> answers)
        {
            foreach (var respondentAnswer in answers)
            {
                respondentAnswer.RespondentId = respondentId;
                _context.Entry(respondentAnswer).Property("CreatedAt").CurrentValue = DateTime.Now;
                _context.RespondentAnswers.Add(respondentAnswer);
            }

            //await _context.SaveChangesAsync();
            return true;
        }
    }
}