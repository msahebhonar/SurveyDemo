using Survey.Entities.Survey;

namespace Survey.Models.Survey
{
    public class RespondentAnswerDto
    {
        public int QuestionBankId { get; set; }

        public string Answer { get; set; }

        public static RespondentAnswer ConvertFromDto(RespondentAnswerDto respondentAnswerDto)
        {
            return new RespondentAnswer
            {
                QuestionBankId = respondentAnswerDto.QuestionBankId,
                Answer = respondentAnswerDto.Answer
            };
        }

        public static RespondentAnswerDto ConvertToDto(RespondentAnswer respondentAnswer)
        {
            return new RespondentAnswerDto
            {
                QuestionBankId = respondentAnswer.QuestionBankId,
                Answer = respondentAnswer.Answer
            };
        }
    }
}