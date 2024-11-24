using LangVet.Application.Features.HighlightedTerms.Query.GetHighlightedTermById;
using LangVet.Application.Features.HighlightedTerms;
using LangVet.Application.Persistence;
using MediatR;

namespace LangVet.Application.Features.Inputs.Query.GetInputById
{
    public class GetInputByIdQueryHandler : IRequestHandler<GetInputByIdQuery, InputDto>
    {
        private readonly IInputRepository repository;

        public GetInputByIdQueryHandler(IInputRepository repository)
        {
            this.repository = repository;
        }

        public async Task<InputDto> Handle(GetInputByIdQuery query, CancellationToken cancellationToken)
        {
            var input = await repository.FindByIdAsync(query.InputId);
            if (input.IsSuccess)
            {
                return new InputDto
                {
                    InputId = query.InputId,
                    InputFile = input.Value.InputFile,
                    InputName = input.Value.InputName,
                    InputText = input.Value.InputText,
                };
            }
            return new InputDto();
        }
    }
}
