using LangVet.Application.Responses;

namespace LangVet.Application.Features.Inputs.Command.DeleteInput
{
    public class DeleteInputCommandResponse : BaseResponse
    {
        public DeleteInputCommandResponse() : base()
        {
            
        }

        public InputDto InputDto { get; set; }
    }
}
