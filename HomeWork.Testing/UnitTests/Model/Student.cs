using System;

namespace Epam.HomeWork.Testing.UnitTests.Model
{
    public class Student : IEquatable<Student>
    {
        public const int MinId = 10000;

        public const int MaxId = 99999;
        
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Student);
        }

        public bool Equals(Student other)
        {
            return other != null &&
                   this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
