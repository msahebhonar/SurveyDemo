using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Survey.Common;
using Survey.Models.Question;
using Survey.Models.Survey;
using Survey.Services;

namespace Survey.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class SurveyController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(IEnumerable<SurveyDetailDto>))]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetSurveyList([FromServices] ISurveyDetailRepository surveyDetailRepository, [Required][FromQuery] string userAccountId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userAccountId))
                    return BadRequest(ErrorMessages.InvalidInput);

                var surveyList = await surveyDetailRepository.GetSurveyListAsync(Guid.Parse(userAccountId));
                var result = surveyList.Select(SurveyDetailDto.ConvertToDto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(QuestionBankDto))]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetSurveyQuestions([FromServices] IQuestionBankRepository questionBankRepository, [Required][FromQuery] string surveyDetailId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(surveyDetailId))
                    return BadRequest(ErrorMessages.InvalidInput);

                var questions = await questionBankRepository.GetQuestionsAsync(Guid.Parse(surveyDetailId));
                var result = questions.Select(QuestionBankDto.ConvertToDto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
