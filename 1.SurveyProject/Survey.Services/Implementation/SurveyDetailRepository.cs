using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Survey.DataAccess;
using Survey.Entities.Survey;

namespace Survey.Services.Implementation
{
    public class SurveyDetailRepository:ISurveyDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public SurveyDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SurveyDetail>> GetSurveyListAsync(Guid userAccountId)
        {
            return await _context.Respondents
                .Where(x => x.UserAccountId == userAccountId && !x.SubmitTime.HasValue)
                .Select(x => x.SurveyDetail)
                .ToListAsync();
        }
    }
}