namespace Back.Models
{
    // Запись
    public class Record
    {
        public int? Id { get; set; }

        public int? ClientId { get; set; }
        public Client? Client { get; set; }

        public int? ServiceId { get; set; }
        public Service? Service { get; set; }

        public int? TimeIntervalId { get; set; }
        public TimeInterval? TimeInterval { get; set; }

        public int? MasterId { get; set; }
        public Master? Master { get; set; }

        public DateTime? Date { get; set; }
        public string? Check { get; set; }
    }
}
