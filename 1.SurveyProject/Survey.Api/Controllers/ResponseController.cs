using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Survey.Common;
using Survey.Models.Survey;
using Survey.Services;

namespace Survey.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ResponseController : ControllerBase
    {
        private readonly ISurveyDetailQuestionRepository _surveyDetailQuestionRepository;
        private readonly IRespondentRepository _respondentRepository;
        private readonly IRespondentAnswerRepository _respondentAnswerRepository;

        public ResponseController(IRespondentRepository respondentRepository, IRespondentAnswerRepository respondentAnswerRepository, ISurveyDetailQuestionRepository surveyDetailQuestionRepository)
        {
            _respondentRepository = respondentRepository;
            _respondentAnswerRepository = respondentAnswerRepository;
            _surveyDetailQuestionRepository = surveyDetailQuestionRepository;
        }

        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(RespondentAnswerDto))]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllRespondentAnswers([FromBody] RespondentInfoDto model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.UserAccountId) || string.IsNullOrWhiteSpace(model.SurveyDetailId))
                    return BadRequest(ErrorMessages.InvalidInput);

                var respondentId = _respondentRepository.GetRespondentId(Guid.Parse(model.UserAccountId),
                Guid.Parse(model.SurveyDetailId));
                if (respondentId == 0)
                    return NotFound(ErrorMessages.InvalidSurveyInfo);

                var answers = await _respondentAnswerRepository.GetAllAnswersAsync(respondentId);
                var results = answers.Select(RespondentAnswerDto.ConvertToDto);

                return Ok(results);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> SaveAndSubmit([FromBody] SaveAndSubmitDto values)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(values.UserAccountId) || string.IsNullOrWhiteSpace(values.SurveyDetailId) || values.RespondentAnswerDto is null)
                    return BadRequest(ErrorMessages.InvalidInput);

                var surveyDetailId = Guid.Parse(values.SurveyDetailId);
                var userAccountId = Guid.Parse(values.UserAccountId);

                if(!IsAllQuestionsAnswered(_surveyDetailQuestionRepository.GetNumberOfQuestionsInSurvey(surveyDetailId), values.RespondentAnswerDto))
                    return StatusCode(403, ErrorMessages.IncompleteForm);

                if (_respondentRepository.IsSubmit(userAccountId, surveyDetailId))
                    return StatusCode(403, ErrorMessages.InvalidOperation);

                var respondentId = _respondentRepository.GetRespondentId(userAccountId,surveyDetailId);
                if (respondentId == 0)
                    return NotFound(ErrorMessages.InvalidSurveyInfo);

                var model = values.RespondentAnswerDto.Select(RespondentAnswerDto.ConvertFromDto);
                var result = _respondentAnswerRepository.SaveAnswersInContext(respondentId, model);
                if (!result) return StatusCode(500, ErrorMessages.UnexpectedError);

                var submit = await _respondentRepository.SubmitSurveyAsync(respondentId);
                return !submit ? StatusCode(500, ErrorMessages.InternalServerError) : CreatedAtAction(nameof(SaveAndSubmit), new { status = 0 });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, ex.Message);
            }
        }


        private bool IsAllQuestionsAnswered(int numberOfQuestionsInSurvey, IEnumerable<RespondentAnswerDto> answerDto)
        {
            var numberOfAnsweredQuestions = answerDto.Select(x => x.QuestionBankId).Distinct().Count();
            return numberOfQuestionsInSurvey == numberOfAnsweredQuestions;
        }
    }
}
