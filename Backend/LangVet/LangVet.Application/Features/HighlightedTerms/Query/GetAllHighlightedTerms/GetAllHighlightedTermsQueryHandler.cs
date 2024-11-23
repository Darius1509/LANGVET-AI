using LangVet.Application.Persistence;
using MediatR;

namespace LangVet.Application.Features.HighlightedTerms.Query.GetAllHighlightedTerms
{
    public class GetAllHighlightedTermsQueryHandler : IRequestHandler<GetAllHighlightedTermsQuery, List<HighlightedTermDto>>
    {
        private readonly IHighlightedTermRepository repository;

        public GetAllHighlightedTermsQueryHandler(IHighlightedTermRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<HighlightedTermDto>> Handle(GetAllHighlightedTermsQuery query, CancellationToken cancellationToken)
        {
            var highlightedTerms = await repository.FindAllAsync();
            var listOfHighlightedTerms = new List<HighlightedTermDto>();

            foreach (var highlightedTerm in highlightedTerms.Value)
            {
                listOfHighlightedTerms.Add(new HighlightedTermDto
                {
                    termName = highlightedTerm.termName,
                    termDefinition = highlightedTerm.termDefinition,
                    termDescription = highlightedTerm.termDescription,
                    termLink = highlightedTerm.termLink,
                    termSubCluster = highlightedTerm.termSubCluster
                });
            }

            return listOfHighlightedTerms;
        }
    }
}
