using MediatR;

namespace LangVet.Application.Features.OutputTerms.Command.DeleteOutputTerms
{
    public class DeleteOutputTermsCommand : IRequest<DeleteOutputTermsCommandResponse>
    {
        public Guid OutputTermsId { get; set; }
    }
}
