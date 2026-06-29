namespace MovieDapperProject.Models
{
    public class Reviews
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public string UserName { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public int ViewCount { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsApproved { get; set; }
    }
}
