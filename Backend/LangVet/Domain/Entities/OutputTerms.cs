using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;

namespace Domain.Entities
{
    public class OutputTerms
    {
        public Guid OutputTermsId { get; private set; }
        public Guid DictionaryId { get; private set; }
        public HighlightedTerm[] HighlightedTerms { get; private set; }

        private OutputTerms(Builder builder)
        {
            OutputTermsId = Guid.NewGuid();
            DictionaryId = builder.DictionaryId != Guid.Empty ? builder.DictionaryId : throw new ArgumentException("DictionaryId cannot be empty.", nameof(builder.DictionaryId));
            HighlightedTerms = builder.HighlightedTerms ?? throw new ArgumentNullException(nameof(builder.HighlightedTerms), "Highlighted terms cannot be null or empty.");
        }

        public class Builder
        {
            public Guid DictionaryId { get; private set; }
            public HighlightedTerm[] HighlightedTerms { get; private set; }

            public Builder SetDictionaryId(Guid dictionaryId)
            {
                DictionaryId = dictionaryId;
                return this;
            }

            public Builder SetHighlightedTerms(List<HighlightedTerm> highlightedTerms)
            {
                if (highlightedTerms == null || highlightedTerms.Count == 0)
                {
                    throw new ArgumentException("Highlighted terms cannot be null or empty.", nameof(highlightedTerms));
                }
                HighlightedTerms = highlightedTerms.ToArray();
                return this;
            }

            public OutputTerms Build()
            {
                return new OutputTerms(this);
            }
        }

        public static Result<OutputTerms> Create(Guid dictionaryId, List<HighlightedTerm> highlightedTerms)
        {
            var builder = new Builder()
                .SetDictionaryId(dictionaryId)
                .SetHighlightedTerms(highlightedTerms);

            return BuildTerms(builder);
        }

        public static Result<OutputTerms> Update(Guid outputTermsId, Guid dictionaryId, List<HighlightedTerm> highlightedTerms)
        {
            var builder = new Builder()
                .SetDictionaryId(dictionaryId)
                .SetHighlightedTerms(highlightedTerms);

            var outputTermsUpdated = builder.Build();
            outputTermsUpdated.OutputTermsId = outputTermsId;

            return Result<OutputTerms>.Success(outputTermsUpdated);
        }

        private static Result<OutputTerms> BuildTerms(Builder builder)
        {
            if (builder.DictionaryId == Guid.Empty)
                return Result<OutputTerms>.Failure("DictionaryId cannot be empty.");

            if (builder.HighlightedTerms == null || builder.HighlightedTerms.Length == 0)
                return Result<OutputTerms>.Failure("Highlighted terms cannot be null or empty.");

            return Result<OutputTerms>.Success(builder.Build());
        }
    }
}
