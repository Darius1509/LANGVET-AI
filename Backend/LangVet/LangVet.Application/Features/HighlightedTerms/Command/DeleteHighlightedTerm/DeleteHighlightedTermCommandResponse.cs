using LangVet.Application.Responses;

namespace LangVet.Application.Features.HighlightedTerms.Command.DeleteHighlightedTerm
{
    public class DeleteHighlightedTermCommandResponse : BaseResponse
    {
        public DeleteHighlightedTermCommandResponse() : base() { }
        
        public HighlightedTermDto HighlightedTermDto { get; set; }
    }
}
