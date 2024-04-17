using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranspoDocMonitor.Service.API.Controllers.Base;
using TranspoDocMonitor.Service.Contracts.DocumentType;
using TranspoDocMonitor.Service.Contracts.TransportDocument.Create;
using TranspoDocMonitor.Service.Contracts.TransportDocument.GetDocument;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.DictionaryDocumentTypes;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.TransportDocuments;

namespace TranspoDocMonitor.Service.API.Controllers.Document
{
    [Authorize(Roles = "member, administrator")]

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

        [HttpPost("/createTransportDocument")]
        public Task<CreateTransportDocumentResponse> Create
        (
            [FromServices] ICreateTransportDocumentHandler handler,
            [FromBody] CreateTransportDocumentRequest request,
            CancellationToken ctn
        )
        {
            return handler.Handle(request, ctn);
        }

        [HttpGet("Get Info")]
        public Task<InfoTransportDocumentResponse[]> GetInfo(
            [FromServices] IGetTransportDocumentHandler handler, 
           [FromQuery] InfoTransportDocumentRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(request, ctn);
        }

    }
}
