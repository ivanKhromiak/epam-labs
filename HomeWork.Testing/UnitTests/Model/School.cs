using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.HomeWork.Testing.UnitTests.Model
{
    public class School
    {
        private readonly List<Course> courses;
        private readonly List<Student> students;

        public School(string name)
        {
            students = new List<Student>();
            courses = new List<Course>();
            this.Name = name;
        }

        public string Name { get; set; }

        public IReadOnlyList<Student> Students => students;

        public IReadOnlyList<Course> Courses => courses;

        public bool AddStudent(Student student)
        {
            if (student.Id < Student.MinId || student.Id > Student.MaxId)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(student),
                    $"Student id has to be between {Student.MinId} and {Student.MaxId}");
            }

            if (string.IsNullOrWhiteSpace(student.FirstName))
            {
                throw new ArgumentException(
                    $"{nameof(student.FirstName)} cannot be null, empty or whitespace",
                    nameof(student));
            }

            if (string.IsNullOrWhiteSpace(student.LastName))
            {
                throw new ArgumentException(
                    $"{nameof(student.LastName)} cannot be null, empty or whitespace",
                    nameof(student));
            }

            bool isAdded = false;

            if (!students.Contains(student))
            {
                students.Add(student);
            }

            return isAdded;
        }

        public bool Remove(Student student)
        {
            return students.Remove(student);
        }

        public Student GetStudent(int stundetId)
        {
            return students.FirstOrDefault(s => s.Id == stundetId);
        }

        public bool AddCourse(string name, int maxStudents)
        {
            var course = new Course(name, maxStudents);
            bool isAdded = false;

            if (!courses.Contains(course))
            {
                courses.Add(course);
            }

            return isAdded;
        }

        public bool RemoveCourseByName(string name)
        {
            return courses.Remove(new Course(name, 0));
        }

        public Course GetCourse(string name)
        {
            return courses.FirstOrDefault(c => c.Name == name);
        }

        public bool AddStudentToTheCourse(string courseName, int studentId)
        {
            if (GetStudent(studentId) == null)
            {
                throw new ArgumentException(
                    $"Stundet with id {studentId} does not belong to school {Name}");
            }

            bool isAdded = false;

            var course = GetCourse(courseName);

            if (course != null)
            {
                isAdded = course.AddStudent(studentId);
            }

            return isAdded;
        }

        public bool RemoveStudentFromTheCourse(string courseName, int studentId)
        {
            if (GetStudent(studentId) == null)
            {
                throw new ArgumentException(
                    $"Stundet with id {studentId} does not belong to school {Name}");
            }

            bool isRemoved = false;

            var course = GetCourse(courseName);

            if (course != null)
            {
                isRemoved = course.RemoveStudent(studentId);
            }

            return isRemoved;
        }
    }
}
