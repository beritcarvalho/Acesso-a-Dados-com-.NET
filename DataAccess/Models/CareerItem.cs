using System;

namespace DataAccess.Models
{
    public class CareerItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Course Course { get; set; }
    }
}

/*
CareerId
CourseId
Title
Description
Order
*/