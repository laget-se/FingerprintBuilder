﻿using laget.Fingerprint.Interfaces;
using System;

namespace laget.Fingerprint.Tests.Models
{
    public class Fingerprint : IFingerprint
    {
        public object Data { get; set; }
        public string Hash { get; set; }
        public object Metadata { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
