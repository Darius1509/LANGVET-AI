using Domain.Common;

namespace Domain.Entities
{
    public class HighlightedTerm
    {
        public HighlightedTerm(string termName, string termDefinition, string termDescription, string termLink, string termSubCluster)
        {
            this.HighlightedTermId = Guid.NewGuid();
            this.termName = termName;
            this.termDefinition = termDefinition;
            this.termDescription = termDescription;
            this.termLink = termLink;
            this.termSubCluster = termSubCluster;
        }

        public Guid HighlightedTermId { get; private set; }
        public string termName { get; private set; }
        public string termDefinition { get; private set; }
        public string termDescription { get; private set; }
        public string termLink { get; private set; }
        public string termSubCluster { get; private set; }

        public static Result<HighlightedTerm> Create(string termName, string termDefinition, string termDescription, string termLink, string termSubCluster)
        {
            var validator = validateParameters(termName, termDefinition, termDescription, termLink, termSubCluster);
            if (validator != null)
            {
                return validator;
            }
            return Result<HighlightedTerm>.Success(new HighlightedTerm(termName, termDefinition, termDescription, termLink, termSubCluster));
        }

        public static Result<HighlightedTerm> Update(Guid termId, string termName, string termDefinition, string termDescription, string termLink, string termSubCluster)
        {
            var validator = validateParameters(termName, termDefinition, termDescription, termLink, termSubCluster);
            if (validator != null)
            {
                return validator;
            }
            var termUpdated = new HighlightedTerm(termName, termDefinition, termDescription, termLink, termSubCluster)
            {
                HighlightedTermId = termId
            };
            return Result<HighlightedTerm>.Success(termUpdated);
        }

        public static Result<HighlightedTerm>? validateParameters(string termName, string termDefinition, string termDescription, string termLink, string termSubCluster)
        {
            if (string.IsNullOrWhiteSpace(termName))
                return Result<HighlightedTerm>.Failure("TermName should not be empty");

            if (string.IsNullOrWhiteSpace(termDefinition))
                return Result<HighlightedTerm>.Failure("TermDefinition should not be empty");

            if (string.IsNullOrWhiteSpace(termDescription))
                return Result<HighlightedTerm>.Failure("TermDescription should not be empty");

            if (string.IsNullOrWhiteSpace(termLink))
                return Result<HighlightedTerm>.Failure("TermLink should not be empty");

            if (string.IsNullOrWhiteSpace(termSubCluster))
                return Result<HighlightedTerm>.Failure("TermSubCluster should not be empty");

            return null;
        }
    }
}
