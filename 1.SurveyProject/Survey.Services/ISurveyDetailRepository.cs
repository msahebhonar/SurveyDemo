using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Survey.Entities.Survey;

namespace Survey.Services
{
    public interface ISurveyDetailRepository
    {
        Task<IEnumerable<SurveyDetail>> GetSurveyListAsync(Guid respondentId);
    }
}
