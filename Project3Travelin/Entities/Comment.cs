using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project3Travelin.Entities
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CommentId { get; set; }
        public string Headline { get; set; }
        public string CommentDetail { get; set; }
        public int Score { get; set; }
        public DateTime CommentDate { get; set; }
        public bool IsStatus { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string TourId { get; set; }

        // Kullanıcı 1 ile 5 arası kaç yıldız verdiyse onu tutuyoruz
        public int Guide { get; set; }
        public int Program { get; set; }
        public int ValueForMoney { get; set; }
        public int Service { get; set; }
        public int Organization { get; set; }

    }
}
