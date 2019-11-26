using System;
using System.Collections.Generic;
using Epam.HomeWork.Testing.UnitTests.Model;
using NUnit.Framework;

namespace Model.Tests
{
    public class SchoolTests
    {    
        [Test]
        public void CannotAddStudentWithNullEmptyOrWhiteSpaceName()
        { 
            School tempSchool = new School("temp");

            Student studentWithNullName = new Student
            {
                Id = Student.MinId,
                FirstName = null,
                LastName = "Test"
            };

            Student studentWithEmptyName = new Student
            {
                Id = Student.MinId,
                FirstName = string.Empty,
                LastName = "Test"
            };

            Student studentWithWhiteSpaceName = new Student
            {
                Id = Student.MinId,
                FirstName = "  ",
                LastName = "Test"
            };

            Assert.Throws<ArgumentException>(() => tempSchool.AddStudent(studentWithNullName));
            Assert.Throws<ArgumentException>(() => tempSchool.AddStudent(studentWithEmptyName));
            Assert.Throws<ArgumentException>(() => tempSchool.AddStudent(studentWithWhiteSpaceName));
        }

        [Test]
        public void CannotAddSameStundetToSchool()
        {
            School tempSchool = new School("temp");

            Student student = new Student
            {
                Id = Student.MinId,
                FirstName = "Test",
                LastName = "Test"
            };

            tempSchool.AddStudent(student);
            bool isAddedSecondTime = tempSchool.AddStudent(student);

            Assert.IsFalse(isAddedSecondTime);
        }

        [Test]
        public void CannotAddMoreThanMaximumInTheCourse()
        {
            var school = new School("Test school");
            string courseName = "Test Course";
            var students = GetTestStudents();

            students.ForEach(s => school.AddStudent(s));
            school.AddCourse(courseName, students.Count - 1);
            var createdCourse = school.GetCourse(courseName);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                students.ForEach(s => createdCourse.AddStudent(s.Id));
            });
        }

        private List<Student> GetTestStudents()
        {
            return new List<Student>()
            {
                new Student
                {
                    Id = Student.MinId + 1,
                    FirstName = "Test",
                    LastName = "Test"
                },
                new Student
                {
                    Id = Student.MinId + 2,
                    FirstName = "Test",
                    LastName = "Test"
                },
                new Student
                {
                    Id = Student.MinId + 3,
                    FirstName = "Test",
                    LastName = "Test"
                }
            };
        }
    }
}