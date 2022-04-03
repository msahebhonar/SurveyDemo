using System.Collections.Generic;
using System.Threading.Tasks;
using Survey.Entities.Survey;

namespace Survey.Services
{
    public interface IRespondentAnswerRepository
    {
        Task<IEnumerable<RespondentAnswer>> GetAllAnswersAsync(int respondentId);

        bool SaveAnswersInContext(int respondentId, IEnumerable<RespondentAnswer> answers);
    }
}