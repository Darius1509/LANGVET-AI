using MediatR;

namespace LangVet.Application.Features.OutputTerms.Query.GetOutputTermsById
{
    public class GetOutputTermsByIdQuery : IRequest<OutputTermsDto>
    {
        public Guid OutputTermsId { get; set; }

        public GetOutputTermsByIdQuery(Guid outputTermsId)
        {
            OutputTermsId = outputTermsId;
        }
    }
}
