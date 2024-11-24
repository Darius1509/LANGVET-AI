using LangVet.Application.Persistence;
using MediatR;

namespace LangVet.Application.Features.OutputTerms.Query.GetOutputTermsById
{
    public class GetOutputTermsByIdQueryHandler : IRequestHandler<GetOutputTermsByIdQuery, OutputTermsDto>
    {
        private readonly IOutputTermsRepository repository;

        public GetOutputTermsByIdQueryHandler(IOutputTermsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<OutputTermsDto> Handle(GetOutputTermsByIdQuery request, CancellationToken cancellationToken)
        {
            var outputTerms = await repository.FindByIdAsync(request.OutputTermsId);
            if(outputTerms.IsSuccess)
            {
                return new OutputTermsDto
                {
                    OutputTermsId = outputTerms.Value.OutputTermsId,
                    DictionaryId = outputTerms.Value.DictionaryId,
                    HighlightedTermIds = outputTerms.Value.HighlightedTermIds
                };
            }
            return new OutputTermsDto();
        }
    }
}
