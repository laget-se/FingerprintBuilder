using System;

namespace laget.Fingerprint.Tests.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime LastActive { get; set; } = DateTime.Now;
    }
}
