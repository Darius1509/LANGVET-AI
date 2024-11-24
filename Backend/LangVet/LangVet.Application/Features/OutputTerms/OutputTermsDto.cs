namespace LangVet.Application.Features.OutputTerms
{
    public class OutputTermsDto
    {
        public Guid OutputTermsId { get; set; }
        public Guid DictionaryId { get; set; }
        public List<Guid> HighlightedTermIds { get; set; }

    }
}
