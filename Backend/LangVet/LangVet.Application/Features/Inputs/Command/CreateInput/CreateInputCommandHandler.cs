using Domain.Entities;
using LangVet.Application.Persistence;
using MediatR;

namespace LangVet.Application.Features.Inputs.Command.CreateInput
{
    public class CreateInputCommandHandler : IRequestHandler<CreateInputCommand, CreateInputCommandResponse>
    {
        private readonly IInputRepository repository;

        public CreateInputCommandHandler(IInputRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateInputCommandResponse> Handle(CreateInputCommand command, CancellationToken cancellationToken)
        {
            var response = new CreateInputCommandResponse();
            var validator = new CreateInputCommandValidator();
            //var validationResult = await validator.ValidateAsync(command, cancellationToken);

            //if(validationResult.Errors.Count > 0)
            //{
            //    response.Success = false;
            //    response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            //    return response;
            //}

            if (response.Success)
            {
                var input = Input.Create(command.InputText, command.InputName, command.InputFile);
                if (input.IsSuccess)
                {
                    await repository.AddAsync(input.Value);
                    response.InputDto = new InputDto
                    {
                        InputName = input.Value.InputName,
                        InputFile = input.Value.InputFile,
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
