using LangVet.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangVet.Application.Features.Inputs.Command.CreateInput
{
    public class CreateInputCommandResponse : BaseResponse
    {
        public CreateInputCommandResponse() : base()
        {
            
        }

        public InputDto InputDto { get; set; }
    }
}
