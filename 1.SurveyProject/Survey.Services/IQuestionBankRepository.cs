using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Survey.Entities.Question;

namespace Survey.Services
{
    public interface IQuestionBankRepository
    {
        Task<IEnumerable<QuestionBank>> GetQuestionsAsync(Guid surveyDetailId);
    }
}