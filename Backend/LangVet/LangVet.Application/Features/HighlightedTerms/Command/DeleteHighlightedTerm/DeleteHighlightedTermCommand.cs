using MediatR;

namespace LangVet.Application.Features.HighlightedTerms.Command.DeleteHighlightedTerm
{
    public class DeleteHighlightedTermCommand : IRequest<DeleteHighlightedTermCommandResponse>
    {
        public Guid HighlightedTermId { get; set; }
    }
}
