using Domain.Entities;
using LangVet.Application.Features.HighlightedTerms.Command.UpdateHighlightedTerm;
using LangVet.Application.Features.HighlightedTerms;
using LangVet.Application.Persistence;
using MediatR;

namespace LangVet.Application.Features.Inputs.Command.UpdateInput
{
    public class UpdateInputCommandHandler : IRequestHandler<UpdateInputCommand, UpdateInputCommandResponse>
    {
        private readonly IInputRepository repository;

        public UpdateInputCommandHandler(IInputRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateInputCommandResponse> Handle(UpdateInputCommand command, CancellationToken cancellationToken)
        {
            var response = new UpdateInputCommandResponse();
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
                var input = Input.Update(command.InputId, command.InputText, command.InputName, command.InputFile);
                if (input.IsSuccess)
                {
                    await repository.UpdateAsync(input.Value);
                    response.InputDto = new InputDto
                    {
                        InputFile = input.Value.InputFile,
                        InputName = input.Value.InputName,
                        InputText = input.Value.InputText
                    };
                }
                else
                {
                    response.Success = false;
                    response.ValidationErrors = new List<string>
                    {
                        input.ErrorMessage
                    };
                }
            }
            return response;
        }
    }
}
