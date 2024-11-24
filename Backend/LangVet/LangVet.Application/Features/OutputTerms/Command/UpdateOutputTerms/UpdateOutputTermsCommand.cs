using MediatR;

namespace LangVet.Application.Features.OutputTerms.Command.UpdateOutputTerms
{
    public class UpdateOutputTermsCommand : IRequest<UpdateOutputTermsCommandResponse>
    {
        public Guid OutputTermsId { get; private set; } 
        public Guid DictionaryId { get; private set; }
        public List<Guid> HighlightedTermIds { get; private set; }
    }
}
