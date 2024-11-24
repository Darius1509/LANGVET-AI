using LangVet.Application.Responses;

namespace LangVet.Application.Features.OutputTerms.Command.CreateOutputTerms
{
    public class CreateOutputTermsCommandResponse : BaseResponse
    {
        public CreateOutputTermsCommandResponse() : base() { }

        public OutputTermsDto OutputTermsDto { get; set; }
    }
}
