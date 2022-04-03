using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Survey.Api.Controllers;
using Survey.Entities.Survey;
using Survey.Models.Survey;
using Survey.Services;
using Xunit;

namespace Survey.UnitTests
{
    public class ResponseTests
    {
        private readonly Mock<ISurveyDetailQuestionRepository> _surveyDetailQuestionMock;
        private readonly Mock<IRespondentRepository> _respondentMock;
        private readonly Mock<IRespondentAnswerRepository> _respondentAnswerMock;
        private readonly ResponseController _responseController;
        private readonly SaveAndSubmitDto _saveAndSubmitMockData;

        public ResponseTests()
        {
            _surveyDetailQuestionMock = new Mock<ISurveyDetailQuestionRepository>();
            _respondentMock = new Mock<IRespondentRepository>();
            _respondentAnswerMock = new Mock<IRespondentAnswerRepository>();
            _responseController =
                new ResponseController(_respondentMock.Object, _respondentAnswerMock.Object, _surveyDetailQuestionMock.Object);
            _saveAndSubmitMockData = new SaveAndSubmitDto
            {
                UserAccountId = Guid.NewGuid().ToString(),
                SurveyDetailId = Guid.NewGuid().ToString(),
                RespondentAnswerDto = new List<RespondentAnswerDto>()
            };
        }

        [Fact]
        public async Task SaveAndSubmit_WhenModelIsNull_BadRequest()
        {
            var result = await _responseController.SaveAndSubmit(new SaveAndSubmitDto());
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task SaveAndSubmit_WhenModelIsValid_GetNumberOfQuestionsInSurveyCalled()
        {
            var result = await _responseController.SaveAndSubmit(_saveAndSubmitMockData);
            _surveyDetailQuestionMock.Verify(_=>_.GetNumberOfQuestionsInSurvey(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task SaveAndSubmit_WhenModelIsValid_IsSubmitCalled()
        {
            await _responseController.SaveAndSubmit(_saveAndSubmitMockData);
            _respondentMock.Verify(_ => _.IsSubmit(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task SaveAndSubmit_WhenSurveySubmittedBefore_Forbid()
        {
            _respondentMock.Setup(_ => _.IsSubmit(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(true);
            var result = await _responseController.SaveAndSubmit(_saveAndSubmitMockData);
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(403, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task SaveAndSubmit_WhenIsNotSubmittedBefore_GetRespondentIdCalled()
        {
            _respondentMock.Setup(_ => _.IsSubmit(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(false);
            var result = await _responseController.SaveAndSubmit(_saveAndSubmitMockData);
            _respondentMock.Verify(_ => _.GetRespondentId(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task SaveAndSubmit_WhenThereIsNoSurveyForUser_NotFound()
        {
            _respondentMock.Setup(_ => _.IsSubmit(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(false);
            _respondentMock.Setup(_ => _.GetRespondentId(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(0);
            var result = await _responseController.SaveAndSubmit(_saveAndSubmitMockData);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task SaveAndSubmit_WhenFoundRespondentId_SaveAnswersInContextCalled()
        {
            _respondentMock.Setup(_ => _.IsSubmit(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(false);
            _respondentMock.Setup(_ => _.GetRespondentId(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(1);
            var result = await _responseController.SaveAndSubmit(_saveAndSubmitMockData);
            _respondentAnswerMock.Verify(_ => _.SaveAnswersInContext(It.IsAny<int>(), It.IsAny<IEnumerable<RespondentAnswer>>()), Times.Once);
        }

        [Fact]
        public async Task SaveAndSubmit_WhenSaveAnswersInContextIsNotSuccessful_InternalServerError()
        {
            _respondentMock.Setup(_ => _.IsSubmit(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(false);
            _respondentMock.Setup(_ => _.GetRespondentId(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(1);
            _respondentAnswerMock
                .Setup(_ => _.SaveAnswersInContext(It.IsAny<int>(), It.IsAny<IEnumerable<RespondentAnswer>>()))
                .Returns(false);
            var result = await _responseController.SaveAndSubmit(_saveAndSubmitMockData);
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task SaveAndSubmit_WhenSaveAnswersInContextIsSuccessful_SubmitSurveyAsyncCalled()
        {
            _respondentMock.Setup(_ => _.IsSubmit(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(false);
            _respondentMock.Setup(_ => _.GetRespondentId(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(1);
            _respondentAnswerMock
                .Setup(_ => _.SaveAnswersInContext(It.IsAny<int>(), It.IsAny<IEnumerable<RespondentAnswer>>()))
                .Returns(true);
            var result = await _responseController.SaveAndSubmit(_saveAndSubmitMockData);
            _respondentMock.Verify(_ => _.SubmitSurveyAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task SaveAndSubmit_WhenSubmitSurveyAsyncIsSuccessful_Ok()
        {
            _respondentMock.Setup(_ => _.IsSubmit(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(false);
            _respondentMock.Setup(_ => _.GetRespondentId(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(1);
            _respondentAnswerMock
                .Setup(_ => _.SaveAnswersInContext(It.IsAny<int>(), It.IsAny<IEnumerable<RespondentAnswer>>()))
                .Returns(true);
            _respondentMock.Setup(_ => _.SubmitSurveyAsync(It.IsAny<int>()).Result).Returns(true);
            var result = await _responseController.SaveAndSubmit(_saveAndSubmitMockData);
            Assert.IsType<CreatedAtActionResult>(result);
        }
    }
}