using LangVet.Application.Responses;

namespace LangVet.Application.Features.HighlightedTerms.Command.UpdateHighlightedTerm
{
    public class UpdateHighlightedTermCommandResponse : BaseResponse
    {
        public UpdateHighlightedTermCommandResponse() : base() { }

        public HighlightedTermDto HighlightedTermDto { get; set; } 
    }
}
