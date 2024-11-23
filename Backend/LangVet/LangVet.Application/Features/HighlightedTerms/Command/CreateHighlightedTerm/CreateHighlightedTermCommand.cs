using MediatR;

namespace LangVet.Application.Features.HighlightedTerms.Command.CreateHighlightedTerm
{
    public class CreateHighlightedTermCommand : IRequest<CreateHighlightedTermCommandResponse>
    {
        public Guid HighlightedTermId { get; private set; }
        public string termName { get; private set; }
        public string termDefinition { get; private set; }
        public string termDescription { get; private set; }
        public string termLink { get; private set; }
        public string termSubCluster { get; private set; }
    }
}
