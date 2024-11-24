using LangVet.Application.Features.HighlightedTerms.Query.GetAllHighlightedTerms;
using LangVet.Application.Features.HighlightedTerms;
using LangVet.Application.Persistence;
using MediatR;

namespace LangVet.Application.Features.Inputs.Query.GetAllInputs
{
    public class GetAllInputsQueryHandler : IRequestHandler<GetAllInputsQuery, List<InputDto>>
    {
        private readonly IInputRepository repository;

        public GetAllInputsQueryHandler(IInputRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<InputDto>> Handle(GetAllInputsQuery query, CancellationToken cancellationToken)
        {
            var inputs = await repository.FindAllAsync();
            var listOfInputs = new List<InputDto>();

            foreach (var input in inputs.Value)
            {
                listOfInputs.Add(new InputDto
                {
                    InputName = input.InputName,
                    InputFile = input.InputFile,
                    InputText = input.InputText
                });
            }

            return listOfInputs;
        }
    }
}
