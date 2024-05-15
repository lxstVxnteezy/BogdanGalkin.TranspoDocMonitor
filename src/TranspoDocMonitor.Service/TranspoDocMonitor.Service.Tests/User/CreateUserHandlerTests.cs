using Moq;
using TranspoDocMonitor.Service.Contracts.User.Create;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.Users;

namespace TranspoDocMonitor.Service.Tests.Handlers
{
    public class CreateUserHandlerTests
    {
        private readonly Mock<IRepository<User>> _userRepositoryMock;
        private readonly Mock<IRepository<Role>> _roleRepositoryMock;
        private readonly CreateUserHandler _createUserHandler;

        public CreateUserHandlerTests()
        {
            _userRepositoryMock = new Mock<IRepository<User>>();
            _roleRepositoryMock = new Mock<IRepository<Role>>();
            _createUserHandler = new CreateUserHandler(_userRepositoryMock.Object, _roleRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateUser_WhenRequestIsValid()
        {
            // Arrange
            var request = new CreateUserRequest(
                Login: "newuser",
                Password: "password123",
                FirstName: "First",
                LastName: "Last",
                Surname: "Surname",
                Email: "newuser@example.com",
                RoleId: Guid.NewGuid()
            );


            _roleRepositoryMock.Setup(r => r.FoundById(request.RoleId)).Returns(new Role());
            _userRepositoryMock.Setup(r => r.Query).Returns(Enumerable.Empty<User>().AsQueryable());

            // Act
            var response = await _createUserHandler.Handle(request, CancellationToken.None);

            // Assert
            _userRepositoryMock.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
            _userRepositoryMock.Verify(r => r.SaveChanges(It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(request.Login, response.login);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRoleDoesNotExist()
        {
            // Arrange
            var request = new CreateUserRequest(
                Login: "newuser",
                Password: "password123",
                FirstName: "First",
                LastName: "Last",
                Surname: "Surname",
                Email: "newuser@example.com",
                RoleId: Guid.NewGuid()
            );

            _roleRepositoryMock.Setup(r => r.FoundById(request.RoleId)).Returns((Role)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<OwnException>(() => _createUserHandler.Handle(request, CancellationToken.None));
            Assert.Equal("Not find role in db", exception.InnerMessage);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenLoginAlreadyExists()
        {
            // Arrange
            var request = new CreateUserRequest(
                Login: "existinguser",
                Password: "password123",
                FirstName: "First",
                LastName: "Last",
                Surname: "Surname",
                Email: "newuser@example.com",
                RoleId: Guid.NewGuid()
            );

            _roleRepositoryMock.Setup(r => r.FoundById(request.RoleId)).Returns(new Role());
            _userRepositoryMock.Setup(r => r.Query).Returns(new[] { new User { Login = request.Login } }.AsQueryable());

            // Act & Assert
            var exception = await Assert.ThrowsAsync<OwnException>(() => _createUserHandler.Handle(request, CancellationToken.None));
            Assert.Equal($"User with login = {request.Login} already exists", exception.InnerMessage);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmailAlreadyExists()
        {
            // Arrange
            var request = new CreateUserRequest(
                Login: "newuser",
                Password: "password123",
                FirstName: "First",
                LastName: "Last",
                Surname: "Surname",
                Email: "existinguser@example.com",
                RoleId: Guid.NewGuid()
            );

            _roleRepositoryMock.Setup(r => r.FoundById(request.RoleId)).Returns(new Role());
            _userRepositoryMock.Setup(r => r.Query).Returns(new[] { new User { Email = request.Email } }.AsQueryable());

            // Act & Assert
            var exception = await Assert.ThrowsAsync<OwnException>(() => _createUserHandler.Handle(request, CancellationToken.None));
            Assert.Equal($"User with email = {request.Email} already exists", exception.InnerMessage);
        }
    }
}
