namespace Domain.Entities
{
    public class Team
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Losses { get; set; }

        public int Points { get; set; }
    }
}
