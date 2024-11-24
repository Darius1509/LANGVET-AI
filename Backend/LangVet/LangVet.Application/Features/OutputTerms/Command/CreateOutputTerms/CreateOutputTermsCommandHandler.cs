using Domain.Entities;
using LangVet.Application.Persistence;
using MediatR;

namespace LangVet.Application.Features.OutputTerms.Command.CreateOutputTerms
{
    public class CreateOutputTermsCommandHandler : IRequestHandler<CreateOutputTermsCommand, CreateOutputTermsCommandResponse>
    {
        private readonly IOutputTermsRepository outputTermsRepository;

        public CreateOutputTermsCommandHandler(IOutputTermsRepository outputTermsRepository)
        {
            this.outputTermsRepository = outputTermsRepository;
        }

        public async Task<CreateOutputTermsCommandResponse> Handle(CreateOutputTermsCommand command, CancellationToken cancellationToken)
        {
            var response = new CreateOutputTermsCommandResponse();
            var validator = new CreateOutputTermsCommandValidator();

            // Uncomment this block if validation is required
            //var validationResult = await validator.ValidateAsync(command, cancellationToken);
            //if (validationResult.Errors.Count > 0)
            //{
              //  response.Success = false;
                //response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                //return response;
            //}

            response.Success = true;

            var outputTermsResult = Domain.Entities.OutputTerms.Create(command.DictionaryId, command.HighlightedTermIds);

            if (outputTermsResult.IsSuccess)
            {
                await outputTermsRepository.AddAsync(outputTermsResult.Value);

                response.OutputTermsDto = new OutputTermsDto
                {
                    OutputTermsId = outputTermsResult.Value.OutputTermsId,
                    DictionaryId = outputTermsResult.Value.DictionaryId,
                    HighlightedTermIds = outputTermsResult.Value.HighlightedTermIds
                };
            }
            else
            {
                response.Success = false;
                response.ValidationErrors = new List<string>
                {
                    outputTermsResult.ErrorMessage
                };
            }

            return response;
        }
    }
}
