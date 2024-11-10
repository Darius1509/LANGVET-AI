using Domain.Common;

namespace Domain.Entities
{
    public class OutputTerms
    {
        public Guid OutputTermsId { get; private set; }
        public Guid DictionaryId { get; private set; }
        public List<Guid> HighlightedTermIds { get; private set; }

        // Constructor for creating a new OutputTerms instance
        public OutputTerms(Guid dictionaryId, List<Guid> highlightedTermIds)
        {
            OutputTermsId = Guid.NewGuid();
            DictionaryId = dictionaryId != Guid.Empty ? dictionaryId : throw new ArgumentException("DictionaryId cannot be empty.", nameof(dictionaryId));
            HighlightedTermIds = highlightedTermIds ?? throw new ArgumentNullException(nameof(highlightedTermIds), "Highlighted term IDs cannot be null or empty.");

            if (HighlightedTermIds.Count == 0)
            {
                throw new ArgumentException("Highlighted term IDs cannot be empty.", nameof(highlightedTermIds));
            }
        }

        // Static factory method for controlled creation of OutputTerms
        public static Result<OutputTerms> Create(Guid dictionaryId, List<Guid> highlightedTermIds)
        {
            if (dictionaryId == Guid.Empty)
                return Result<OutputTerms>.Failure("DictionaryId cannot be empty.");

            if (highlightedTermIds == null || highlightedTermIds.Count == 0)
                return Result<OutputTerms>.Failure("Highlighted term IDs cannot be null or empty.");

            return Result<OutputTerms>.Success(new OutputTerms(dictionaryId, highlightedTermIds));
        }

        // Static method for controlled updates to OutputTerms
        public static Result<OutputTerms> Update(Guid outputTermsId, Guid dictionaryId, List<Guid> highlightedTermIds)
        {
            if (dictionaryId == Guid.Empty)
                return Result<OutputTerms>.Failure("DictionaryId cannot be empty.");

            if (highlightedTermIds == null || highlightedTermIds.Count == 0)
                return Result<OutputTerms>.Failure("Highlighted term IDs cannot be null or empty.");

            var updatedOutputTerms = new OutputTerms(dictionaryId, highlightedTermIds)
            {
                OutputTermsId = outputTermsId // Preserve the existing ID
            };

            return Result<OutputTerms>.Success(updatedOutputTerms);
        }
    }
}
