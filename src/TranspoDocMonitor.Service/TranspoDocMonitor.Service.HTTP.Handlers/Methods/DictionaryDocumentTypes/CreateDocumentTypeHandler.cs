using TranspoDocMonitor.Service.Contracts.DocumentType;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Library.Dictionaries;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.DictionaryDocumentTypes
{
    public interface ICreateDocumentTypeHandler : IHandler
    {
        public Task<CreateDocumentTypeResponse> Handle(CreateDocumentTypeRequest request, CancellationToken ctn);
    }


    public class CreateDocumentTypeHandler : ICreateDocumentTypeHandler
    {
        private readonly IRepository<DictionaryDocumentType> _dictionaryDocumentTypeRepository;

        public CreateDocumentTypeHandler(IRepository<DictionaryDocumentType> dictionaryDocumentTypeRepository)
        {
            _dictionaryDocumentTypeRepository = dictionaryDocumentTypeRepository;
        }

        public async Task<CreateDocumentTypeResponse> Handle(CreateDocumentTypeRequest request, CancellationToken ctn)
        {
            AssertExistDocumentType(request);
            var newDocumentType = new DictionaryDocumentType()
            {
                Id = Guid.NewGuid(),
                DocumentName = request.DocumentName,
            };
            _dictionaryDocumentTypeRepository.Add(newDocumentType);
            await _dictionaryDocumentTypeRepository.SaveChanges(ctn);

            return new CreateDocumentTypeResponse(Id: newDocumentType.Id);
        }

        private void AssertExistDocumentType(CreateDocumentTypeRequest request)
        {
            var isExistDocumentType = _dictionaryDocumentTypeRepository.Query.Any(x => x.DocumentName == request.DocumentName);
            if (isExistDocumentType)
                throw OwnError
                    .CanNotCreateDocumentType
                    .ToException
                    ($"Document with name " +
                     $"{request.DocumentName} already exist");
        }
    }
}
