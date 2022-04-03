using Microsoft.AspNetCore.Mvc;
using Moq;
using Survey.Api.Controllers;
using Survey.Entities.User;
using Survey.Models.Login;
using Survey.Services;
using Xunit;

namespace Survey.UnitTests
{
    public class UserAccountTests
    {
        private readonly Mock<IUserAccountRepository> _userAccountMock;
        private readonly UserAccountController _userAccountController;

        public UserAccountTests()
        {
            _userAccountMock = new Mock<IUserAccountRepository>();
            _userAccountController = new UserAccountController();
        }

        [Fact]
        public void Login_WhenEmailOrPasswordIsNull_BadRequest()
        {
            var result = _userAccountController.Login(_userAccountMock.Object, new LoginDto());
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Login_WhenEmailOrPasswordIsNotNull_LoginCalled()
        {
            _userAccountController.Login(_userAccountMock.Object, new LoginDto { Email = "Test", Password = "Test" });
            _userAccountMock.Verify(_ => _.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Login_WhenEmailOrPasswordIsInvalid_UnAuthorized()
        {
            _userAccountMock.Setup(_ => _.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((UserAccount)null);
            var result = _userAccountController.Login(_userAccountMock.Object, new LoginDto{Email = "Test", Password = "Test"});
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public void Login_WhenEmailOrPasswordIsValid_Ok()
        {
            _userAccountMock.Setup(_ => _.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(new UserAccount());
            var result = _userAccountController.Login(_userAccountMock.Object, new LoginDto{Email = "Test", Password = "Test"});
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
