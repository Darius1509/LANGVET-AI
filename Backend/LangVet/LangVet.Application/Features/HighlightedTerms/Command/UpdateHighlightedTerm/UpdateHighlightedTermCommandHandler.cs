using Domain.Entities;
using LangVet.Application.Persistence;
using MediatR;

namespace LangVet.Application.Features.HighlightedTerms.Command.UpdateHighlightedTerm
{
    public class UpdateHighlightedTermCommandHandler : IRequestHandler<UpdateHighlightedTermCommand, UpdateHighlightedTermCommandResponse>
    {
        private readonly IHighlightedTermRepository repository;

        public UpdateHighlightedTermCommandHandler(IHighlightedTermRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateHighlightedTermCommandResponse> Handle(UpdateHighlightedTermCommand command, CancellationToken cancellationToken)
        {
            var response = new UpdateHighlightedTermCommandResponse();
            //var validator = new UpdateHighlightedTermCommandValidator();
            //var validationResult = await validator.ValidateAsync(command, cancellationToken);
            //if (validationResult.Errors.Count > 0)
            //{
            //    response.Success = false;
            //    response.ValidationErrors = new List<string>();
            //    foreach (var error in validationResult.Errors)
            //    {
            //        response.ValidationErrors.Add(error.ErrorMessage);
            //    }
            //}
            if (response.Success)
            {
                var highlightedTerm = HighlightedTerm.Update(command.HighlightedTermId, command.termName, command.termDefinition, command.termDescription, command.termLink, command.termSubCluster);
                if (highlightedTerm.IsSuccess)
                {
                    await repository.UpdateAsync(highlightedTerm.Value);
                    response.HighlightedTermDto = new HighlightedTermDto
                    {
                        termName = highlightedTerm.Value.termName,
                        termDefinition = highlightedTerm.Value.termDefinition,
                        termDescription = highlightedTerm.Value.termDescription,
                        termLink = highlightedTerm.Value.termLink,
                        termSubCluster = highlightedTerm.Value.termSubCluster
                    };
                }
                else
                {
                    response.Success = false;
                    response.ValidationErrors = new List<string>
                    {
                        highlightedTerm.ErrorMessage
                    };
                }
            }
            return response;
        }
    }
}
