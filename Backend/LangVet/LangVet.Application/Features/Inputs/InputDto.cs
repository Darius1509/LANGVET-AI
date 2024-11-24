namespace LangVet.Application.Features.Inputs
{
    public class InputDto
    {
        public Guid InputId { get; set; }

        public byte[] InputFile { get; set; }

        public string InputText { get; set; }

        public string InputName { get;  set; }
    }
}
