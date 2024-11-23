using MediatR;

namespace LangVet.Application.Features.HighlightedTerms.Query.GetAllHighlightedTerms
{
    public class GetAllHighlightedTermsQuery : IRequest<List<HighlightedTermDto>>
    {

    }
}
