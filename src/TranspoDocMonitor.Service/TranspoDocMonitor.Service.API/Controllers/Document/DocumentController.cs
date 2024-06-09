using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranspoDocMonitor.Service.API.Controllers.Base;
using TranspoDocMonitor.Service.Contracts.DocumentType;
using TranspoDocMonitor.Service.Contracts.Pass.Create;
using TranspoDocMonitor.Service.Contracts.Pass.Get;
using TranspoDocMonitor.Service.Contracts.Pass.Update;
using TranspoDocMonitor.Service.Contracts.TransportDocument.Create;
using TranspoDocMonitor.Service.Contracts.TransportDocument.GetDocument;
using TranspoDocMonitor.Service.Contracts.User.Info;
using TranspoDocMonitor.Service.Contracts.User.Update;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.DictionaryDocumentTypes;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.Passes;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.TransportDocuments;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.Users;


namespace TranspoDocMonitor.Service.API.Controllers.Document
{
    [Authorize(Roles = "member, administrator")]

    [Route("api/document")]
    public class DocumentController : BaseApiController
    {
        [HttpPost("/createTypeDocument")]
        public Task<CreateDocumentTypeResponse> Create
        (
            [FromServices] ICreateDocumentTypeHandler handler,
            [FromBody] CreateDocumentTypeRequest request,
            CancellationToken ctn
        )
        {
            return handler.Handle(request, ctn);
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

        [HttpGet("/GetInfo")]
        public Task<InfoTransportDocumentResponse[]> GetInfo(
            [FromServices] IGetTransportDocumentHandler handler,
           [FromQuery] InfoTransportDocumentRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(request, ctn);
        }
        [HttpPost("{id}/createPass")]
        public Task<CreatePassResponse> Update(
            [FromServices] ICreatePassHandler handler,
            [FromRoute] Guid id,
            [FromBody] CreatePassRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(id, request, ctn);
        }

        [HttpDelete("/deletePass{id}")]
        public Task<ActionResult> Delete(
            [FromServices] IDeletePassHandler handler,
            [FromRoute] Guid id,
            CancellationToken ctn)
        {
            return handler.Handle(id, ctn);
        }

        [HttpPut("{id}/updatePass")]
        public Task<UpdatePassResponse> Update(
            [FromServices] IUpdatePassHandler handler,
            [FromRoute] Guid id,
            [FromBody] UpdatePassRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(id, request, ctn);
        }

        [HttpGet("{email}/GetPass")]
        public Task<GetPassResponse[]> GetById(
            [FromServices] IGetPassHandler handler,
            [FromRoute] string email,
            CancellationToken ctn)
        {
            return handler.Handle(email, ctn);
        }
    }
}
