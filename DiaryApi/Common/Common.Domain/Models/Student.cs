﻿using System.Collections.Generic;

namespace Common.Domain.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string ParentEmail { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<StudentResult> StudentResults { get; set; }
    }
}
