using Domain.Common;

namespace Domain.Entities
{
    public class OutputTerms
    {

        public OutputTerms(Guid dictionaryId, List<Guid> highlightedTermIds)
        {
            OutputTermsId = Guid.NewGuid();
            DictionaryId = dictionaryId;
            HighlightedTermIds = highlightedTermIds;
        }

        public Guid OutputTermsId { get; private set; }
        public Guid DictionaryId { get; private set; }
        public List<Guid> HighlightedTermIds { get; private set; }

        public static Result<OutputTerms> Create(Guid dictionaryId, List<Guid> highlightedTermIds)
        {
            if (dictionaryId == Guid.Empty)
                return Result<OutputTerms>.Failure("DictionaryId cannot be empty.");

            if (highlightedTermIds == null || highlightedTermIds.Count == 0)
                return Result<OutputTerms>.Failure("Highlighted term IDs cannot be null or empty.");

            return Result<OutputTerms>.Success(new OutputTerms(dictionaryId, highlightedTermIds));
        }

        public static Result<OutputTerms> Update(Guid outputTermsId, Guid dictionaryId, List<Guid> highlightedTermIds)
        {
            if (dictionaryId == Guid.Empty)
                return Result<OutputTerms>.Failure("DictionaryId cannot be empty.");

            if (highlightedTermIds == null || highlightedTermIds.Count == 0)
                return Result<OutputTerms>.Failure("Highlighted term IDs cannot be null or empty.");

            var updatedOutputTerms = new OutputTerms(dictionaryId, highlightedTermIds)
            {
                OutputTermsId = outputTermsId
            };

            return Result<OutputTerms>.Success(updatedOutputTerms);
        }
    }
}
