using LangVet.Application.Features.OutputTerms;
using LangVet.Application.Responses;

namespace LangVet.Application.Features.OutputTerms.Command.DeleteOutputTerms
{
    public class DeleteOutputTermsCommandResponse : BaseResponse
    { 
        public DeleteOutputTermsCommandResponse() : base() { }

        public OutputTermsDto OutputTermsDto { get; set; }
    }
}
