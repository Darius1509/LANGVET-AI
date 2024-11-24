using MediatR;

namespace LangVet.Application.Features.OutputTerms.Query.GetAllOutputTerms
{
    public class GetAllOutputTermsQuery : IRequest<List<OutputTermsDto>>
    {
    }
}
