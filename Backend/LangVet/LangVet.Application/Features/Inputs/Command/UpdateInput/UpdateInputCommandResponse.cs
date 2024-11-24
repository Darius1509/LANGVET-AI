using LangVet.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangVet.Application.Features.Inputs.Command.UpdateInput
{
    public class UpdateInputCommandResponse : BaseResponse
    {
        public UpdateInputCommandResponse() : base()
        { 

        }

        public InputDto InputDto { get; set; }
    }
}
