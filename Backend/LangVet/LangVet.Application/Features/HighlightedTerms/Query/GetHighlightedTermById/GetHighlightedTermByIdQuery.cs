using MediatR;

namespace LangVet.Application.Features.HighlightedTerms.Query.GetHighlightedTermById
{
    public class GetHighlightedTermByIdQuery : IRequest<HighlightedTermDto>
    {
        public Guid HighlightedTermId { get; set; }

        public GetHighlightedTermByIdQuery(Guid highlightedTermId)
        {
            HighlightedTermId = highlightedTermId;
        }
    }
}
