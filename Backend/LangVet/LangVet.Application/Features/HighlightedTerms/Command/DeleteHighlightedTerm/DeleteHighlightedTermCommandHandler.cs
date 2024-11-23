using LangVet.Application.Persistence;
using MediatR;

namespace LangVet.Application.Features.HighlightedTerms.Command.DeleteHighlightedTerm
{
    public class DeleteHighlightedTermCommandHandler : IRequestHandler<DeleteHighlightedTermCommand, DeleteHighlightedTermCommandResponse>
    {
        private readonly IHighlightedTermRepository repository;

        public DeleteHighlightedTermCommandHandler(IHighlightedTermRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteHighlightedTermCommandResponse> Handle(DeleteHighlightedTermCommand command, CancellationToken cancellationToken)
        {
            var response = new DeleteHighlightedTermCommandResponse();
            var highlightedTerm = await repository.DeleteAsync(command.HighlightedTermId);

            if (!highlightedTerm.IsSuccess)
            {
                response.Success = false;
                response.Message = "Deletion was unsuccessful";

                return response;
            }

            response.Success = true;
            response.HighlightedTermDto = new HighlightedTermDto
            {
                termName = highlightedTerm.Value.termName,
                termDefinition = highlightedTerm.Value.termDefinition,
                termDescription = highlightedTerm.Value.termDescription,
                termLink = highlightedTerm.Value.termLink,
                termSubCluster = highlightedTerm.Value.termSubCluster
            };

            return response;
        }
    }
}
