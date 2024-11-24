using LangVet.Application.Features.OutputTerms.Command.DeleteOutputTerms;
using LangVet.Application.Features.OutputTerms;
using LangVet.Application.Persistence;
using MediatR;

namespace LangVet.Application.Features.OutputTermss.Command.DeleteOutputTerms
{
    public class DeleteOutputTermsCommandHandler : IRequestHandler<DeleteOutputTermsCommand, DeleteOutputTermsCommandResponse>
    {
        private readonly IOutputTermsRepository repository;

        public DeleteOutputTermsCommandHandler(IOutputTermsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteOutputTermsCommandResponse> Handle(DeleteOutputTermsCommand command, CancellationToken cancellationToken)
        {
            var response = new DeleteOutputTermsCommandResponse();
            var OutputTerms = await repository.DeleteAsync(command.OutputTermsId);

            if (!OutputTerms.IsSuccess)
            {
                response.Success = false;
                response.Message = "Deletion was unsuccessful";

                return response;
            }

            response.Success = true;
            response.OutputTermsDto = new OutputTermsDto
            {
                OutputTermsId = OutputTerms.Value.OutputTermsId,
                DictionaryId = OutputTerms.Value.DictionaryId,
                HighlightedTermIds = OutputTerms.Value.HighlightedTermIds
            };

            return response;
        }

    }
}
