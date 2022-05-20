namespace ProjectApplication.Models
{
    public class CreateNoteRequest
    {
        public string Text { get; set; }
        public int? ProjectId { get; set; }
        public List<int>? Attributes { get; set; }
    }
}
