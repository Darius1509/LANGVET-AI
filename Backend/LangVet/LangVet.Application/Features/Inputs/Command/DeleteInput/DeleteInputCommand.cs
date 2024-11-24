using MediatR;

namespace LangVet.Application.Features.Inputs.Command.DeleteInput
{
    public class DeleteInputCommand : IRequest<DeleteInputCommandResponse>
    {
        public Guid inputId { get; set; }
    }
}
