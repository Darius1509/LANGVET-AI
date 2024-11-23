using LangVet.Application.Responses;

namespace LangVet.Application.Features.HighlightedTerms.Command.CreateHighlightedTerm
{
    public class CreateHighlightedTermCommandResponse : BaseResponse
    {
        public CreateHighlightedTermCommandResponse() : base() { }

        public HighlightedTermDto HighlightedTermDto { get; set; }
    }
}
