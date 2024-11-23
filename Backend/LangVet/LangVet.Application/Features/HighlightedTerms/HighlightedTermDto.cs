namespace LangVet.Application.Features.HighlightedTerms
{
    public class HighlightedTermDto
    {
        public Guid HighlightedTermId { get; set; }
        public string termName { get; set; }
        public string termDefinition { get; set; }
        public string termDescription { get; set; }
        public string termLink { get; set; }
        public string termSubCluster { get; set; }
    }
}
