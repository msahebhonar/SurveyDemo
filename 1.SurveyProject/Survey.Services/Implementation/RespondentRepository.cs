using System;
using System.Linq;
using System.Threading.Tasks;
using Survey.DataAccess;

namespace Survey.Services.Implementation
{
    public class RespondentRepository : IRespondentRepository
    {
        private readonly ApplicationDbContext _context;

        public RespondentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int GetRespondentId(Guid userAccountId, Guid surveyDetailId)
        {
            var respondent = _context.Respondents.SingleOrDefault(x => x.UserAccountId == userAccountId && x.SurveyDetailId == surveyDetailId);
            return respondent?.RespondentId ?? 0;
        }

        public async Task<bool> SubmitSurveyAsync(int respondentId)
        {
            var respondent = _context.Respondents.SingleOrDefault(x => x.RespondentId == respondentId);
            if (respondent == null) return false;
            respondent.SubmitTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public bool IsSubmit(Guid userAccountId, Guid surveyDetailId)
        {
            var respondent = _context.Respondents.SingleOrDefault(x =>
                 x.UserAccountId == userAccountId && x.SurveyDetailId == surveyDetailId && x.SubmitTime.HasValue);
            return respondent is not null;
        }
    }
}