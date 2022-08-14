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
                      enterprise.Employees.Any(employee => employee.Salary >= 1000)
                      );
        }
    }
}