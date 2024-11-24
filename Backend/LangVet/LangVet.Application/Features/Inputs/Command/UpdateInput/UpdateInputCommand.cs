using MediatR;

namespace LangVet.Application.Features.Inputs.Command.UpdateInput
{
    public class UpdateInputCommand : IRequest<UpdateInputCommandResponse>
    {
        public Guid InputId { get; private set; }

        public byte[] InputFile { get; private set; }

        public string InputText { get; private set; }

        public string InputName { get; private set; }
    }
}
