using FluentValidation;

namespace LangVet.Application.Features.Inputs.Command.CreateInput
{
    public class CreateInputCommandValidator : AbstractValidator<CreateInputCommand>
    {
        public CreateInputCommandValidator()
        {
            RuleFor(x => x.InputId)
                .NotEmpty().WithMessage("InputId is required.");

            RuleFor(x => x.InputFile)
                .NotEmpty().WithMessage("InputFile is required.")
                .Must(HaveValidSize).WithMessage("InputFile size must not exceed 10 MB.");

            RuleFor(x => x.InputText)
                .NotEmpty().WithMessage("InputText is required.")
                .MaximumLength(1000).WithMessage("InputText cannot exceed 1000 characters.");

            RuleFor(x => x.InputName)
                .NotEmpty().WithMessage("InputName is required.")
                .MaximumLength(100).WithMessage("InputName cannot exceed 100 characters.");
        }

        private bool HaveValidSize(byte[] inputFile)
        {
            const int maxFileSizeInBytes = 10 * 1024 * 1024; // 10 MB
            return inputFile.Length <= maxFileSizeInBytes;
        }
    }
}
