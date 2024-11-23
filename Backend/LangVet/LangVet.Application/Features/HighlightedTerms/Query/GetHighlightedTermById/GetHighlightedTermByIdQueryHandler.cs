using LangVet.Application.Persistence;
using MediatR;

namespace LangVet.Application.Features.HighlightedTerms.Query.GetHighlightedTermById
{
    public class GetHighlightedTermByIdQueryHandler : IRequestHandler<GetHighlightedTermByIdQuery, HighlightedTermDto>
    {
        private readonly IHighlightedTermRepository repository;

        public GetHighlightedTermByIdQueryHandler(IHighlightedTermRepository repository)
        {
            this.repository = repository;
        }

        public async Task<HighlightedTermDto> Handle(GetHighlightedTermByIdQuery query, CancellationToken cancellationToken)
        {
            var highlightedTerm = await repository.FindByIdAsync(query.HighlightedTermId);
            if (highlightedTerm.IsSuccess)
            {
                return new HighlightedTermDto
                {
                    HighlightedTermId = query.HighlightedTermId,
                    termName =  highlightedTerm.Value.termName,
                    termDefinition = highlightedTerm.Value.termDefinition,
                    termDescription = highlightedTerm.Value.termDescription,
                    termLink = highlightedTerm.Value.termLink,
                    termSubCluster = highlightedTerm.Value.termSubCluster
                };
            }
            return new HighlightedTermDto();
        }
    }
}
