using MediatR;

namespace LangVet.Application.Features.OutputTerms.Command.CreateOutputTerms
{
    public class CreateOutputTermsCommand : IRequest<CreateOutputTermsCommandResponse>
    {
        public Guid OutputTermsId { get; private set; } 
        public Guid DictionaryId { get; private set; }
        public List<Guid> HighlightedTermIds { get; private set; }
    }
}
