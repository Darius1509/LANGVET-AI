using Domain.Common;

namespace Domain.Entities
{
    public class Input
    {
        public Input(string inputText, string inputName, byte[] inputFile)
        {
            InputId = Guid.NewGuid();
            InputText = inputText;
            InputName = inputName;
            InputFile = inputFile;

        }

        public Guid InputId { get; private set; }

        public byte[] InputFile { get; private set; }

        public string InputText { get; private set; }

        public string InputName { get; private set; }

        public static Result<Input> Create(string inputText, string inputName, byte[] inputFile)
        {
            var validator = validateParameters(inputText, inputName, inputFile);
            if (validator != null) 
            {
                return validator;
            }
            return Result<Input>.Success(new Input(inputText, inputName, inputFile));
        }

        public static Result<Input> Update(Guid inputId, string inputText, string inputName, byte[] inputFile)
        {
            var validator = validateParameters(inputText, inputName, inputFile);
            if (validator != null)
            {
                return validator;
            }
            var inputUpdated = new Input(inputText, inputName, inputFile)
            {
                InputId = inputId
            };
            return Result<Input>.Success(inputUpdated);
        }

        public static Result<Input>? validateParameters(string inputText, string inputName, byte[] inputFile)
        {
            if (string.IsNullOrWhiteSpace(inputText))
                return Result<Input>.Failure("InputText should not be empty");

            if (string.IsNullOrWhiteSpace(inputName))
                return Result<Input>.Failure("InputName should not be empty");

            if (inputFile.Length == 0)
                return Result<Input>.Failure("InputFile should not be empty");

            return null;
        }

    }
}
