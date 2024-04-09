namespace UserNotesSystem.Core.Entities
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string OwnerId { get; set; }
    }
}
