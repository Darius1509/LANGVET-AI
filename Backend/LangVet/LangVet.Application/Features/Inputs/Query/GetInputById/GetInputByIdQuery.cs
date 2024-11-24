using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangVet.Application.Features.Inputs.Query.GetInputById
{
    public class GetInputByIdQuery : IRequest<InputDto>
    {
        public Guid InputId { get; set; }

        public GetInputByIdQuery(Guid inputId) 
        { 
            InputId = inputId;
        }
    }
}
