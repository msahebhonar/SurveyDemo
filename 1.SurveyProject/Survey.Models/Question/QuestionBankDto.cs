using System.Collections.Generic;
using System.Linq;
using Survey.Common;
using Survey.Entities.Question;

namespace Survey.Models.Question
{
    public class QuestionBankDto
    {
        public int QuestionBankId { get; set; }

        public string Text { get; set; }

        public QuestionType QuestionType { get; set; }

        public ICollection<ResponseDto> Responses { get; set; }

        public static QuestionBankDto ConvertToDto(QuestionBank questionBank)
        {
            return new QuestionBankDto
            {
                QuestionBankId = questionBank.QuestionBankId,
                Text = questionBank.Text,
                QuestionType = questionBank.QuestionType,
                Responses = questionBank.Responses.Select(ResponseDto.ConvertToDto).ToList()
            };
        }
    }
}