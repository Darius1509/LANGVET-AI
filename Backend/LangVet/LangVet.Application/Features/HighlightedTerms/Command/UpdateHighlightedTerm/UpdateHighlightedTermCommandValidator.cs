using FluentValidation;

namespace LangVet.Application.Features.HighlightedTerms.Command.UpdateHighlightedTerm
{
    public class UpdateHighlightedTermCommandValidator : AbstractValidator<UpdateHighlightedTermCommand>
    {

        public UpdateHighlightedTermCommandValidator()
        {
            RuleFor(x => x.HighlightedTermId)
                .NotEmpty().WithMessage("HighlightedTermId is required.");

            RuleFor(x => x.termName)
                .NotEmpty().WithMessage("Term Name is required.")
                .MaximumLength(100).WithMessage("Term Name cannot exceed 100 characters.");

            RuleFor(x => x.termDefinition)
                .NotEmpty().WithMessage("Term Definition is required.")
                .MaximumLength(500).WithMessage("Term Definition cannot exceed 500 characters.");

            RuleFor(x => x.termDescription)
                .MaximumLength(1000).WithMessage("Term Description cannot exceed 1000 characters.");

            RuleFor(x => x.termLink)
                .MaximumLength(200).WithMessage("Term Link cannot exceed 200 characters.")
                .Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.termLink))
                .WithMessage("Term Link must be a valid URL.");

            RuleFor(x => x.termSubCluster)
                .MaximumLength(100).WithMessage("Term Sub-Cluster cannot exceed 100 characters.");
        }

        private bool BeAValidUrl(string? termLink)
        {
            return Uri.TryCreate(termLink, UriKind.Absolute, out _);
        }
    }
}
