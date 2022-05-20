namespace ProjectApplication.Models
{
    public class GetNotesRequest
    {
        public int? ProjectId { get; set; }
        public List<int>? AttributeIds { get; set; }
    }
}
