using TranspoDocMonitor.Service.Contracts.DocumentType;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.DictionaryDocumentTypes
{
    public interface ICreateDocumentTypeHandler : IHandler
    {
        public Task<CreateDocumentTypeResponse> Handle(CreateDocumentTypeRequest request, CancellationToken ctn);
    }


    public class CreateDocumentTypeHandler : ICreateDocumentTypeHandler
    {
        public Task<CreateDocumentTypeResponse> Handle(CreateDocumentTypeRequest request, CancellationToken ctn)
        {
            throw new NotImplementedException();
        }
    }
}
