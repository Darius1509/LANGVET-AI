using LangVet.Application.Persistence;
using MediatR;

namespace LangVet.Application.Features.Inputs.Command.DeleteInput
{
    public class DeleteInputCommandHandler : IRequestHandler<DeleteInputCommand, DeleteInputCommandResponse>
    {
        private readonly IInputRepository repository;

        public DeleteInputCommandHandler(IInputRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteInputCommandResponse> Handle(DeleteInputCommand command, CancellationToken cancellationToken)
        {
            var response = new DeleteInputCommandResponse();
            var input = await repository.DeleteAsync(command.inputId);

            if (!input.IsSuccess)
            {
                response.Success = false;
                response.Message = "Deletion was unsuccessful";

                return response;
            }

            response.Success = true;
            response.InputDto = new InputDto
            {
                InputFile = input.Value.InputFile,
                InputName = input.Value.InputName,
                InputText = input.Value.InputText
            };

            return response;
        }
    }
}
