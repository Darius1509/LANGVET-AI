using FluentValidation;

namespace LangVet.Application.Features.OutputTerms.Command.UpdateOutputTerms
{
    public class UpdateOutputTermsCommandValidator : AbstractValidator<UpdateOutputTermsCommand>
    {
        public UpdateOutputTermsCommandValidator()
        {
            RuleFor(x => x.OutputTermsId)
                .NotEmpty().WithMessage("OutputTermsId is required.");

            RuleFor(x => x.DictionaryId)
                .NotEmpty().WithMessage("DictionaryId is required.");

            RuleFor(x => x.HighlightedTermIds)
                .NotNull().WithMessage("HighlightedTermIds list is required.")
                .NotEmpty().WithMessage("HighlightedTermIds list cannot be empty.")
                .Must(HaveValidIds).WithMessage("HighlightedTermIds list contains invalid or empty GUIDs.")
                .Must(ids => ids.Count <= 100).WithMessage("HighlightedTermIds list cannot exceed 100 items.");

            // Optional: Validate minimum count if required
            RuleFor(x => x.HighlightedTermIds)
                .Must(ids => ids.Count >= 1).WithMessage("HighlightedTermIds list must contain at least 1 item.");
        }

        private bool HaveValidIds(List<Guid> ids)
        {
            return ids.All(id => id != Guid.Empty);
        }
    }
}
