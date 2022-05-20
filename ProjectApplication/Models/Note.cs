namespace ProjectApplication.Models
{
    public class Note
    {
        public DateTime Created { get; set; }
        public string Text { get; set; }
        public int ProjectId { get; set; }
        //public string Attributes { get; set; }
        public List<int> Attributes { get; set; }
    }
}
