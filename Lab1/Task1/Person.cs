using System;

namespace Epam.HomeWork.Lab1.Task1
{
    public struct Person
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        /// <summary>
        /// Gets a string that represents a Person age compared to some value
        /// </summary>
        /// <param name="age">Some age</param>
        /// <returns>string</returns>
        /// <example>
        /// Can return:
        ///     "{Name} {Surname} same as {age}"
        ///     "{Name} {Surname} older than {age}"
        ///     "{Name} {Surname} younger than {age}"
        /// </example>
        public string OlderThan(int age)
        {
            if(age <= 0)
            {
                throw new ArgumentException("Age cannot be zero or less");
            }

            string ageString;
            if (Age == age)
            {
                ageString = "same as";
            }
            else
            {
                ageString = Age >= age ? "older than" : "younger than";
            }

            return $"{Name} {Surname} {ageString} {age}";
        }
    } 
}
