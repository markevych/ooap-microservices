﻿using Common.Domain.Enums;

namespace Common.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public byte[] Image { get; set; }
        public UserRole UserRole { get; set; }
        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
    }
}
