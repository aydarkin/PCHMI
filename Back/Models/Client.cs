using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Back.Models
{
    public class Client
    {
        public int? Id { get; set; }

        public string? Surname { get; set; }

        public string? Name { get; set; }
        public string? Patronymic { get; set; }
        public string? Gender { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool? HasTelegram { get; set; }
        public bool? HasWhatsapp { get; set; }
        public bool? IsVIP { get; set; }
    }
}