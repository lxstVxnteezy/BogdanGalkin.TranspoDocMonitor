using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranspoDocMonitor.Service.API.Controllers.Base;
using TranspoDocMonitor.Service.Contracts.Pass.Create;
using TranspoDocMonitor.Service.Contracts.Pass.Get;
using TranspoDocMonitor.Service.Contracts.Pass.Update;
using TranspoDocMonitor.Service.Contracts.TransportDocument.Create;
using TranspoDocMonitor.Service.Contracts.TransportDocument.GetDocument;
using TranspoDocMonitor.Service.Contracts.TransportDocument.Update;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.Passes;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.TransportDocuments;


namespace TranspoDocMonitor.Service.API.Controllers.Document
{
    [Authorize(Roles = "member, administrator")]

    [Route("api/document")]
    public class DocumentController : BaseApiController
    {


        [HttpPost("{id}/createTransportDocument")]
        public Task<CreateTransportDocumentResponse> Create
        (
            [FromServices] ICreateTransportDocumentHandler handler,
            [FromRoute] Guid id,
            [FromBody] CreateTransportDocumentRequest request,
            CancellationToken ctn
        )
        {
            return handler.Handle(id, request, ctn);
        }

        [HttpGet("{email}/getInfoTransportDocument")]
        public Task<InfoTransportDocumentResponse[]> GetInfo(
            [FromServices] IGetTransportDocumentHandler handler,
            [FromRoute] string email,
            CancellationToken ctn)
        {
            return handler.Handle(email, ctn);
        }

        [HttpDelete("/deleteTransportDocument{id}")]
        public Task<ActionResult> Delete(
            [FromServices] IDeleteTransportDocumentHandler handler,
            [FromRoute] Guid id,
            CancellationToken ctn)
        {
            return handler.Handle(id, ctn);
        }

        [HttpPut("{id}/updateTransportDocument")]
        public Task<UpdateTransportDocumentResponse> Update(
            [FromServices] IUpdateTransportDocumentHandler handler,
            [FromRoute] Guid id,
            [FromBody] UpdateTransportDocumentRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(id, request, ctn);
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

        [HttpGet("{email}/getPass")]
        public Task<GetPassResponse[]> GetById(
            [FromServices] IGetPassHandler handler,
            [FromRoute] string email,
            CancellationToken ctn)
        {
            return handler.Handle(email, ctn);
        }



    }
}
