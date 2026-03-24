namespace Domain.Entities
{
    public class Team
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public int? FoundedYear { get; set; }

        // Navigation properties to matches - keep as collections but avoid eager loading by default
        public List<Match>? HomeMatches { get; set; }

        public List<Match>? AwayMatches { get; set; }
    }
}
