using LangVet.Application.Responses;

namespace LangVet.Application.Features.OutputTerms.Command.UpdateOutputTerms
{
    public class UpdateOutputTermsCommandResponse : BaseResponse
    {
        public UpdateOutputTermsCommandResponse() : base() { }

        public OutputTermsDto OutputTermsDto { get; set; }
    }
}
