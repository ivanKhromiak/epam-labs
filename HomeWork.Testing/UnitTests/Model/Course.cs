using System;
using System.Collections.Generic;

namespace Epam.HomeWork.Testing.UnitTests.Model
{
    public class Course : IEquatable<Course>
    {
        public Course(string name, int maxStudents)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Course name cannot be null, empty or whitespace",
                    nameof(name));
            }

            StudentsId = new HashSet<int>();
            this.Name = name;
            this.MaxStudents = maxStudents;
        }

        public string Name { get; set; }

        public int MaxStudents { get; }

        public HashSet<int> StudentsId { get; }

        public bool AddStudent(int studentId)
        {
            if (StudentsId.Count + 1 > MaxStudents)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(studentId),
                    $"There can only be maximum {MaxStudents} in the {Name} course");
            }

            return StudentsId.Add(studentId);
        }

        public bool RemoveStudent(int studentId)
        {
            return StudentsId.Remove(studentId);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Course);
        }

        public bool Equals(Course other)
        {
            return other != null &&
                   this.Name == other.Name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
