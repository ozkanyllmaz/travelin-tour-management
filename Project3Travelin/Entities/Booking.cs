using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Project3Travelin.Models.Enums;

namespace Project3Travelin.Entities
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookingId { get; set; }
        public string TourId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


        public int PassengerCount { get; set; } //kaç kişi katılacak
        public DateTime BookingDate { get; set; }

        [BsonRepresentation(BsonType.String)]
        public BookingStatus BookingStatus { get; set; }

    }
}
