using LangVet.Application.Persistence;
using MediatR;

namespace LangVet.Application.Features.OutputTerms.Query.GetAllOutputTerms
{
    public class GetAllOutputTermsQueryHandler : IRequestHandler<GetAllOutputTermsQuery, List<OutputTermsDto>>
    {
        private readonly IOutputTermsRepository repository;

        public GetAllOutputTermsQueryHandler(IOutputTermsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<OutputTermsDto>> Handle(GetAllOutputTermsQuery request, CancellationToken cancellationToken)
        {
            var outputTerms = await repository.FindAllAsync();
            var listOfOutputTerms = new List<OutputTermsDto>();

            foreach (var outputTerm in outputTerms.Value)
            {
                listOfOutputTerms.Add(new OutputTermsDto
                {
                    OutputTermsId = outputTerm.OutputTermsId,
                    DictionaryId = outputTerm.DictionaryId,
                    HighlightedTermIds = outputTerm.HighlightedTermIds
                });
            }

            return listOfOutputTerms;
        }
    }
}
