using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqSnippets
{
    public class Snippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "FIat Punto",
                "Fiat Ibiza",
                "Seat Leon"
            };

            //1. SELECT * of cars (SELECT * FROM CARS)
            var carList = from car in cars select car;

            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            //2. SELECT WHERE car is Audi (SELECT * FROM CARS WHERE CARS ARE CALLED AUDIS)
            var audilist = from car in cars where car.Contains("Audi") select car;

            foreach (var audi in audilist)
            {
                Console.WriteLine(audi);
            }

        }

        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var processedNumberList =
                numbers
                    .Select(num => num * 3) //Multiplos de 3
                    .Where(num => num != 9) // All but 9
                    .OrderBy(num => num); // Order ascendent
        }

        static public void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };

            //First of all
            var first = textList.First();

            //First element that is "C"
            var Ctext = textList.First(text => text.Equals("c"));

            //First element containing "J"
            var Jtext = textList.First(text => text.Contains('j'));

            //First element containig Z or default
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains('z'));

            //last element containig Z or default
            var lasttOrDefaultText = textList.LastOrDefault(text => text.Contains('z'));

            //Single values
            var uniqueTexts = textList.Single();
            var uniqueOrDefaultTexts = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6, };

            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers);
        }


        static void MultipleSelects()
        {
            //SELECT MANY
            string[] myOpinions =
            {
            "Opinion 1; text 1",
            "Opinion 2; text 2",
            "Opinion 3; text 3"

            };

            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new []
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Annabella",
                            Email = "bella@gmail.com",
                            Salary = 1000
                        },

                        new Employee
                        {
                            Id = 2,
                            Name = "July",
                            Email = "july@gmail.com",
                            Salary = 2000
                        },

                        new Employee
                        {
                            Id = 3,
                            Name = "Guillermo",
                            Email = "guillermo@gmail.com",
                            Salary = 3000
                        }
                    }
                },

                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new []
                    {
                        new Employee
                        {
                            Id = 4,
                            Name = "Herli",
                            Email = "Herli@gmail.com",
                            Salary = 1500
                        },

                        new Employee
                        {
                            Id = 5,
                            Name = "Fabiana",
                            Email = "Fabiana@gmail.com",
                            Salary = 2500
                        },

                        new Employee
                        {
                            Id = 6,
                            Name = "Daniel",
                            Email = "Daniel@gmail.com",
                            Salary = 3500
                        }
                    }
                }
            };


            //Obtain all employees from all enterprises
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            //Know if any list is empty
            bool hasEnterprises = enterprises.Any();
            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            //Employees with at least 1000 salary from all enterprises
            bool hasEmployeesWithSalaryMoreThanOrEqual1000 =
                enterprises.Any(enterprise =>
                      enterprise.Employees.Any(employee => employee.Salary >= 1000));

        }

        static public void linqCollections()
        {
            var firstList = new List<string> { "a", "b", "c" };
            var secondList = new List<string> { "d", "e", "f" };

            //INNER JOIN
            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(secondList,
                firstElement => firstElement,
                secondElement => secondElement,
                (firstElement, secondElement) => new { firstElement, secondElement });

            //OUTTER JOIN LEFT
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };

            //OUTTER JOIN RIGHT
            var rightOuterJoin = from secondElement in secondList
                                 join element in firstList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };

            //UNION
            var unionList = leftOuterJoin.Union(rightOuterJoin);

        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };

            //SKIP
            var skipTwoFirstValue = myList.Skip(2); // {3, 4, 5, 6, 7, 8, 9, 10}

            var skipTwoLastValue = myList.Skip(2); // {1, 2, 3, 4, 5, 6, 7, 8}

            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4); // {4, 5, 6, 7, 8, 9, 10}

            //TAKE
            var takeTwoFirstValue = myList.Take(2); // {1, 2}

            var takeLastTwoValues = myList.TakeLast(2); // {9, 10}

            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); // {1, 2, 3}
        }

        //PAGING
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultPerPage)
        {
            int startIndex = (pageNumber - 1) * resultPerPage;
            return collection.Skip(startIndex).Take(resultPerPage);
        }

        //VARIABLES
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquare = Math.Pow(number, 2)
                               where nSquare > average
                               select number;

            Console.WriteLine("Average: {0}", numbers.Average());

            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Query Number: {0} Square: {1}", number, Math.Pow(number, 2));
            }
        }

        //ZIP
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + " = " + word);
            //{1 = one, 2 = two, 3 = three, 4 = four, 5 = five}
        }

        //REPEAT & RANGE
        static public void RepeatRangeLinq()
        {
            //GENERATE COLLECTION OF VALUES IE 1-1000 --> RANGE
            IEnumerable<int> first100 = Enumerable.Range(1, 100);

            //REPEAT A VALUE N TIMES
            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5); // "X","X","X","X","X","X"


        }

        static public void StudentsLinq()
        {
            var classRoom = new[]
            {
                new Student()
                {
                    Id = 1,
                    Name = "Annabella",
                    Grade = 100,
                    Certified = true
                },
                new Student()
                {
                    Id = 2,
                    Name = "July",
                    Grade = 90,
                    Certified = true
                },
                new Student()
                {
                    Id = 3,
                    Name = "Guillermo",
                    Grade = 80,
                    Certified = true
                },
                new Student()
                {
                    Id = 4,
                    Name = "Herli",
                    Grade = 40,
                    Certified = false

                },
                new Student()
                {
                    Id = 5,
                    Name = "Fabiana",
                    Grade = 30,
                    Certified = false

                },
                new Student()
                {
                    Id = 6,
                    Name = "Daniel",
                    Grade = 45,
                    Certified = false

                }
            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student.Name + student.Grade;

            var notCertifiedStudents = from student in classRoom
                                       where !student.Certified
                                       select student.Name + student.Grade;

            var approvedStudents = from student in classRoom
                                   where student.Grade > 50 && student.Certified
                                   select student.Name;
        }

        //ALL
        static public void AllLinq()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5 };
            bool allNumbersAreEven = numbers.All(num => num % 2 == 0); //false
            bool allNumbersAreOdd = numbers.All(num => num % 2 == 1); //false
            bool allAreSmallerThan10 = numbers.All(num => num < 10); //true
            bool allAreBiggerOrEqualThan2 = numbers.All(num => num >= 2); //false

            var emptyList = new List<int>();
            bool allNumbersAreGreaterThan0 = emptyList.All(num => num > 0); //true
        }

        //AGGREGATE
        static public void AggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //SUM ALL NUMBERS
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current); //55 secuencia fibonachi

            string[] words = { "Hello,", "my", "name", "is", "Guillermo" };
            string sentence = words.Aggregate((prevSentence, current) => prevSentence + " " + current); //"Hello, my name is Guillermo"

        }
        //DISTINCT
        static public void Distincvalue()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 2, 3, 4 };
            IEnumerable<int> distinctNumbers = numbers.Distinct(); //{1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
        }

        //GROUPBY
        static public void GroupByExamples()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //OBTAIN ONLY EVEN NUMBERS
            var evenNumbers = numbers.Where(num => num % 2 == 0); //{2, 4, 6, 8, 10}

            //OBTAIN ONLY ODD NUMBERS
            var oddNumbers = numbers.Where(num => num % 2 == 1); //{1, 3, 5, 7, 9}

            //OBTAIN ONLY NUMBERS GREATER THAN 5
            var numbersGreaterThan5 = numbers.Where(num => num > 5); //{6, 7, 8, 9, 10}

            //OBTAIN ONLY NUMBERS GREATER THAN 5 AND EVEN
            var numbersGreaterThan5AndEven = numbers.Where(num => num > 5 && num % 2 == 0); //{6, 8, 10}

            var classRoom = new[]
            {
                new Student()
                {
                    Id = 1,
                    Name = "Annabella",
                    Grade = 100,
                    Certified = true
                },
                new Student()
                {
                    Id = 2,
                    Name = "July",
                    Grade = 90,
                    Certified = true
                },
                new Student()
                {
                    Id = 3,
                    Name = "Guillermo",
                    Grade = 80,
                    Certified = true
                },
                new Student()
                {
                    Id = 4,
                    Name = "Herli",
                    Grade = 40,
                    Certified = false

                },
                new Student()
                {
                    Id = 5,
                    Name = "Fabiana",
                    Grade = 30,
                    Certified = false

                },
                new Student()
                {
                    Id = 6,
                    Name = "Daniel",
                    Grade = 45,
                    Certified = false

                }
            };

            var certifiedQuerie = classRoom.GroupBy(student => student.Certified);
            //we obtain 2 groups:
            //1-Not certified
            //2-Certified

            foreach (var group in certifiedQuerie)
            {
                Console.WriteLine("-------- {0} -------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name);
                }
            }
        }

        static public void relationsLinq()
        {
            List<Post> posts = new();
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>
                    {
                        new Comment()
                        {
                            Id = 1,
                            Title = "My first innner Title",
                            Content = "My first innner content",
                            Created = DateTime.Now
                        },
                         new Comment()
                        {
                            Id = 2,
                            Title = "My second innner Title",
                            Content = "My second innner content",
                            Created = DateTime.Now
                        }
                    }

                };

                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My second content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>
                    {
                        new Comment()
                        {
                            Id = 3,
                            Title = "My third innner Title",
                            Content = "My third innner content",
                            Created = DateTime.Now
                        },
                         new Comment()
                        {
                            Id = 4,
                            Title = "My fourth innner Title",
                            Content = "My fourth innner content",
                            Created = DateTime.Now
                         }
                    }

                };
            };

            var commentContent = posts.SelectMany(
                post => post.Comments,
                    (post, comment) => new { PostId = post.Id, CommentContent = comment.Content });
        }


    }
}