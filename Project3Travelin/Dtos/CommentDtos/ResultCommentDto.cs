namespace Project3Travelin.Dtos.CommentDtos
{
    public class ResultCommentDto
    {
        public string CommentId { get; set; }
        public string Headline { get; set; }
        public string CommentDetail { get; set; }
        public int Score { get; set; }
        public DateTime CommentDate { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsStatus { get; set; }
        public int Guide { get; set; }
        public int Program { get; set; }
        public int ValueForMoney { get; set; }
        public int Service { get; set; }
        public int Organization { get; set; }
    }
}
