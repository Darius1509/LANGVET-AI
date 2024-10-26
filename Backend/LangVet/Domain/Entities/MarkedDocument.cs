using System;
using Domain.Common;

namespace Domain.Entities
{
    public class MarkedDocument
    {
        public Guid MarkedDocId { get; private set; }
        public Input InputDocument { get; private set; }
        public Guid DictionaryId { get; private set; }
        public string FileDownloadLink { get; private set; }

        private MarkedDocument(Builder builder)
        {
            MarkedDocId = Guid.NewGuid();
            InputDocument = builder.InputDocument ?? throw new ArgumentNullException(nameof(builder.InputDocument), "InputDocument cannot be null.");
            DictionaryId = builder.DictionaryId != Guid.Empty ? builder.DictionaryId : throw new ArgumentException("DictionaryId cannot be empty.", nameof(builder.DictionaryId));
            FileDownloadLink = !string.IsNullOrWhiteSpace(builder.FileDownloadLink) ? builder.FileDownloadLink : throw new ArgumentException("FileDownloadLink cannot be empty.", nameof(builder.FileDownloadLink));
        }

        public class Builder
        {
            public Input InputDocument { get; private set; }
            public Guid DictionaryId { get; private set; }
            public string FileDownloadLink { get; private set; }

            public Builder SetInputDocument(Input inputDocument)
            {
                InputDocument = inputDocument;
                return this;
            }

            public Builder SetDictionaryId(Guid dictionaryId)
            {
                DictionaryId = dictionaryId;
                return this;
            }

            public Builder SetFileDownloadLink(string fileDownloadLink)
            {
                FileDownloadLink = fileDownloadLink;
                return this;
            }

            public MarkedDocument Build()
            {
                return new MarkedDocument(this);
            }
        }

        public static Result<MarkedDocument> Create(Input inputDocument, Guid dictionaryId, string fileDownloadLink)
        {
            var builder = new Builder()
                .SetInputDocument(inputDocument)
                .SetDictionaryId(dictionaryId)
                .SetFileDownloadLink(fileDownloadLink);

            return BuildDocument(builder);
        }

        public static Result<MarkedDocument> Update(Guid inputDocId, Input inputDocument, Guid dictionaryId, string fileDownloadLink)
        {
            var builder = new Builder()
                .SetInputDocument(inputDocument)
                .SetDictionaryId(dictionaryId)
                .SetFileDownloadLink(fileDownloadLink);

            var markedDocUpdated = builder.Build();
            markedDocUpdated.MarkedDocId = inputDocId;

            return Result<MarkedDocument>.Success(markedDocUpdated);
        }

        private static Result<MarkedDocument> BuildDocument(Builder builder)
        {
            if (builder.InputDocument == null)
                return Result<MarkedDocument>.Failure("InputDocument cannot be null.");

            if (builder.DictionaryId == Guid.Empty)
                return Result<MarkedDocument>.Failure("DictionaryId cannot be empty.");

            if (string.IsNullOrWhiteSpace(builder.FileDownloadLink))
                return Result<MarkedDocument>.Failure("FileDownloadLink cannot be empty.");

            return Result<MarkedDocument>.Success(builder.Build());
        }
    }
}
