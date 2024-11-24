using MediatR;

namespace LangVet.Application.Features.Inputs.Command.CreateInput
{
    public class CreateInputCommand : IRequest<CreateInputCommandResponse>
    {
        public Guid InputId { get; private set; }

        public byte[] InputFile { get; private set; }

        public string InputText { get; private set; }

        public string InputName { get; private set; }
    }
}
