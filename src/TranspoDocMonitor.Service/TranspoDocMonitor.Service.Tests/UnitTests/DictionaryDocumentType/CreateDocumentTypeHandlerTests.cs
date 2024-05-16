using Moq;
using TranspoDocMonitor.Service.Contracts.DocumentType;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.DictionaryDocumentTypes;

namespace TranspoDocMonitor.Service.Tests.UnitTests.DictionaryDocumentType
{
    public class CreateDocumentTypeHandlerTests
    {
        private readonly Mock<IRepository<Domain.Library.Dictionaries.DictionaryDocumentType>> _dictionaryDocumentTypeRepositoryMock;
        private readonly CreateDocumentTypeHandler _createDocumentTypeHandler;

        public CreateDocumentTypeHandlerTests()
        {
            _dictionaryDocumentTypeRepositoryMock = new Mock<IRepository<Domain.Library.Dictionaries.DictionaryDocumentType>>();
            _createDocumentTypeHandler = new CreateDocumentTypeHandler(_dictionaryDocumentTypeRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateDocumentType_WhenRequestIsValid()
        {
            //Arrange
            var request = new CreateDocumentTypeRequest
            (
                DocumentName: "Passport"
            );

            _dictionaryDocumentTypeRepositoryMock.Setup(r => r.Query)
                .Returns(Enumerable.Empty<Domain.Library.Dictionaries.DictionaryDocumentType>()
                    .AsQueryable());

            //Act
            var response = await _createDocumentTypeHandler.Handle(request, CancellationToken.None);

            //Asserts 
            _dictionaryDocumentTypeRepositoryMock.Verify(r => r.Add(It.IsAny<Domain.Library.Dictionaries.DictionaryDocumentType>()), Times.Once);
            _dictionaryDocumentTypeRepositoryMock.Verify(r => r.SaveChanges(It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(response);
            Assert.NotEqual(Guid.Empty, response.Id);

        }
        
        [Fact]
        public async Task Handle_ShouldCreateDocumentType_ShouldThrowException()
        {
            //Arrange
            var request = new CreateDocumentTypeRequest
            (
                DocumentName: "Passport"
            );

            _dictionaryDocumentTypeRepositoryMock.Setup(r => r.Query)
                .Returns(new[]
                    {
                        new Domain.Library.Dictionaries.DictionaryDocumentType { DocumentName = request.DocumentName }
                    }
                    .AsQueryable());
            //Act
            var exception = await Assert.ThrowsAsync<OwnException>(() => _createDocumentTypeHandler.Handle(request, CancellationToken.None));
           
            //Asserts 
            Assert.Equal($"Document with name {request.DocumentName} already exist", exception.InnerMessage);
        }
    }
}
