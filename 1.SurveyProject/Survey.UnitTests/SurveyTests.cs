using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Survey.Api.Controllers;
using Survey.Entities.Question;
using Survey.Entities.Survey;
using Survey.Services;
using Xunit;

namespace Survey.UnitTests
{
    public class SurveyTests
    {
        private readonly Mock<ISurveyDetailRepository> _surveyDetailMock;
        private readonly Mock<IQuestionBankRepository> _questionBankRepositoryMock;
        private readonly SurveyController _surveyController;

        public SurveyTests()
        {
            _surveyDetailMock = new Mock<ISurveyDetailRepository>();
            _questionBankRepositoryMock = new Mock<IQuestionBankRepository>();
            _surveyController = new SurveyController();
        }

        [Fact]
        public async Task GetSurveyList_WhenUserAccountIsNull_BadRequest()
        {
            var result = await _surveyController.GetSurveyList(_surveyDetailMock.Object, null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetSurveyList_WhenUserAccountIsNotNull_GetSurveyListAsyncCalled()
        {
            await _surveyController.GetSurveyList(_surveyDetailMock.Object, Guid.NewGuid().ToString());
            _surveyDetailMock.Verify(_ => _.GetSurveyListAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetSurveyList_WhenUserAccountIsValid_Ok()
        {
            _surveyDetailMock.Setup(_ => _.GetSurveyListAsync(It.IsAny<Guid>()).Result).Returns(new List<SurveyDetail>());
            var result = await _surveyController.GetSurveyList(_surveyDetailMock.Object, Guid.NewGuid().ToString());
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetSurveyQuestions_WhenSurveyDetailIdIsNull_BadRequest()
        {
            var result = await _surveyController.GetSurveyQuestions(_questionBankRepositoryMock.Object, null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetSurveyList_WhenSurveyDetailIdIsNotNull_GetQuestionsAsyncCalled()
        {
            await _surveyController.GetSurveyQuestions(_questionBankRepositoryMock.Object, Guid.NewGuid().ToString());
            _questionBankRepositoryMock.Verify(_ => _.GetQuestionsAsync(It.IsAny<Guid>()), Times.Once);
        }


        [Fact]
        public async Task GetSurveyQuestions_WhenSurveyDetailIdIsValid_Ok()
        {
            _questionBankRepositoryMock.Setup(_ => _.GetQuestionsAsync(It.IsAny<Guid>()).Result).Returns(new List<QuestionBank>());
            var result = await _surveyController.GetSurveyQuestions(_questionBankRepositoryMock.Object, Guid.NewGuid().ToString());
            Assert.IsType<OkObjectResult>(result);
        }
    }
}