using Domain.Common;

namespace Domain.Entities
{
    public class MarkedDocument
    {
        public Guid MarkedDocumentId { get; private set; }
        public Guid InputDocumentId { get; private set; }
        public Guid DictionaryId { get; private set; }
        public string FileDownloadLink { get; private set; }

        // Public constructor for creating a new MarkedDocument instance
        public MarkedDocument(Guid inputDocumentId, Guid dictionaryId, string fileDownloadLink)
        {
            MarkedDocumentId = Guid.NewGuid();
            InputDocumentId = inputDocumentId != Guid.Empty ? inputDocumentId : throw new ArgumentException("InputDocumentId cannot be empty.", nameof(inputDocumentId));
            DictionaryId = dictionaryId != Guid.Empty ? dictionaryId : throw new ArgumentException("DictionaryId cannot be empty.", nameof(dictionaryId));
            FileDownloadLink = !string.IsNullOrWhiteSpace(fileDownloadLink) ? fileDownloadLink : throw new ArgumentException("FileDownloadLink cannot be empty.", nameof(fileDownloadLink));
        }

        // Static factory method for controlled creation of a MarkedDocument
        public static Result<MarkedDocument> Create(Guid inputDocumentId, Guid dictionaryId, string fileDownloadLink)
        {
            if (inputDocumentId == Guid.Empty)
                return Result<MarkedDocument>.Failure("InputDocumentId cannot be empty.");

            if (dictionaryId == Guid.Empty)
                return Result<MarkedDocument>.Failure("DictionaryId cannot be empty.");

            if (string.IsNullOrWhiteSpace(fileDownloadLink))
                return Result<MarkedDocument>.Failure("FileDownloadLink cannot be empty.");

            return Result<MarkedDocument>.Success(new MarkedDocument(inputDocumentId, dictionaryId, fileDownloadLink));
        }

        // Static method for controlled updates to a MarkedDocument
        public static Result<MarkedDocument> Update(Guid markedDocId, Guid inputDocumentId, Guid dictionaryId, string fileDownloadLink)
        {
            if (inputDocumentId == Guid.Empty)
                return Result<MarkedDocument>.Failure("InputDocumentId cannot be empty.");

            if (dictionaryId == Guid.Empty)
                return Result<MarkedDocument>.Failure("DictionaryId cannot be empty.");

            if (string.IsNullOrWhiteSpace(fileDownloadLink))
                return Result<MarkedDocument>.Failure("FileDownloadLink cannot be empty.");

            var updatedMarkedDocument = new MarkedDocument(inputDocumentId, dictionaryId, fileDownloadLink)
            {
                MarkedDocumentId = markedDocId  // Preserve the existing ID
            };

            return Result<MarkedDocument>.Success(updatedMarkedDocument);
        }
    }
}
