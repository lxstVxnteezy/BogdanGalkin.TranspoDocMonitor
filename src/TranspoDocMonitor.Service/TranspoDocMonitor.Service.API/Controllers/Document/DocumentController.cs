using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranspoDocMonitor.Service.API.Controllers.Base;
using TranspoDocMonitor.Service.Contracts.DocumentType;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.DictionaryDocumentTypes;

namespace TranspoDocMonitor.Service.API.Controllers.Document
{

    [Route("api/document")]
    public class DocumentController:BaseApiController
    {
        [HttpPost("/createTypeDocument")]
        public Task<CreateDocumentTypeResponse> Create
        (
            [FromServices] ICreateDocumentTypeHandler handler,
            [FromBody] CreateDocumentTypeRequest request,
            CancellationToken ctn
        )
        {
            return handler.Handle( request, ctn );
        }
    }
}
