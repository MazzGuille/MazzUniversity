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

        //REPEAT

        //ALL

        //AGGREGATE

        //DISTINCT

        //GROUPBY


    }
}