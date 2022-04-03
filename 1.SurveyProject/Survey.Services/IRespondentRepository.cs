using System;
using System.Threading.Tasks;

namespace Survey.Services
{
    public interface IRespondentRepository
    {
        int GetRespondentId(Guid userAccountId, Guid surveyDetailId);

        Task<bool> SubmitSurveyAsync(int respondentId);

        bool IsSubmit(Guid userAccountId, Guid surveyDetailId);
    }
}