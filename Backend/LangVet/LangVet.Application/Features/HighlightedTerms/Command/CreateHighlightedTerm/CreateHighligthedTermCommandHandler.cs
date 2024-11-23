using Domain.Entities;
using LangVet.Application.Persistence;
using MediatR;

namespace LangVet.Application.Features.HighlightedTerms.Command.CreateHighlightedTerm
{
    public class CreateHighligthedTermCommandHandler : IRequestHandler<CreateHighlightedTermCommand, CreateHighlightedTermCommandResponse>
    {
        private readonly IHighlightedTermRepository highlightedTermRepository;

        public CreateHighligthedTermCommandHandler(IHighlightedTermRepository highlightedTermRepository)
        {
            this.highlightedTermRepository = highlightedTermRepository;
        }

        public async Task<CreateHighlightedTermCommandResponse> Handle(CreateHighlightedTermCommand command, CancellationToken cancellationToken) 
        { 
            var response = new CreateHighlightedTermCommandResponse();
            var validator = new CreateHighlightedTermCommandValidator();
            //var validationResult = await validator.ValidateAsync(command, cancellationToken);

            //if(validationResult.Errors.Count > 0)
            //{
            //    response.Success = false;
            //    response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            //    return response;
            //}

            if (response.Success) 
            {
                var highlightedTerm = HighlightedTerm.Create(command.termName, command.termDefinition, command.termDescription, command.termLink, command.termSubCluster);
                if (highlightedTerm.IsSuccess) 
                {
                    await highlightedTermRepository.AddAsync(highlightedTerm.Value);
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
