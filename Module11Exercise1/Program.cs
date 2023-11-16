using System;
using System.Collections.Generic;
using System.Linq;

interface Employees
{
    string Name { get; set; }
    string Position { get; set; }
    DateTime HireDate { get; set; }
    decimal Salary { get; set; }
    string Gender { get; set; }
}

class Employee : Employees
{
    public string Name { get; set; }
    public string Position { get; set; }
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public string Gender { get; set; }

    public override string ToString() =>
        $"Name: {Name}, Position: {Position}, Hire Date: {HireDate.ToShortDateString()}, Salary: {Salary:C}, Gender: {Gender}";
}

class Program
{
    static void Main()
    {
        Console.Write("Enter the number of employees: ");
        int numberOfEmployees = int.Parse(Console.ReadLine());

        List<Employees> employees = new List<Employees>();

        for (int i = 0; i < numberOfEmployees; i++)
        {
            Console.WriteLine($"Enter details for employee #{i + 1}:");
            employees.Add(ReadEmployeeFromConsole());
        }

        PrintAllEmployees(employees);

        Console.Write("Enter the position to filter employees: ");
        string positionFilter = Console.ReadLine();
        PrintEmployeesByPosition(employees, positionFilter);

        PrintManagersAboveAverageClerkSalary(employees);

        Console.Write("Enter the hire date filter (yyyy-MM-dd): ");
        DateTime hireDateFilter = DateTime.Parse(Console.ReadLine());
        PrintEmployeesHiredAfterDate(employees, hireDateFilter);

        Console.Write("Enter gender to filter (or press Enter for all): ");
        string genderFilter = Console.ReadLine();
        PrintEmployeesByGender(employees, genderFilter);
    }

    static void PrintAllEmployees(List<Employees> employees)
    {
        Console.WriteLine("\nAll Employees:");
        foreach (var employee in employees)
        {
            Console.WriteLine(employee);
        }
    }

    static void PrintEmployeesByPosition(List<Employees> employees, string position)
    {
        Console.WriteLine($"\nEmployees with Position '{position}':");
        foreach (var employee in employees.Where(e => e.Position.Equals(position, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine(employee);
        }
    }

    static void PrintManagersAboveAverageClerkSalary(List<Employees> employees)
    {
        decimal clerkAverageSalary = employees
            .Where(e => e.Position.Equals("clerk", StringComparison.OrdinalIgnoreCase))
            .Average(e => e.Salary);

        var managersAboveAverageSalary = employees
            .Where(e => e.Position.Equals("manager", StringComparison.OrdinalIgnoreCase) && e.Salary > clerkAverageSalary)
            .OrderBy(e => e.Name);

        Console.WriteLine("\nManagers with Salary above Average Clerk Salary:");
        foreach (var manager in managersAboveAverageSalary)
        {
            Console.WriteLine(manager);
        }
    }

    static void PrintEmployeesHiredAfterDate(List<Employees> employees, DateTime hireDateFilter)
    {
        var filteredEmployees = employees
            .Where(e => e.HireDate > hireDateFilter)
            .OrderBy(e => e.Name);

        Console.WriteLine($"\nEmployees Hired After {hireDateFilter.ToShortDateString()}:");
        foreach (var employee in filteredEmployees)
        {
            Console.WriteLine(employee);
        }
    }

    static void PrintEmployeesByGender(List<Employees> employees, string genderFilter)
    {
        var filteredEmployees = string.IsNullOrEmpty(genderFilter)
            ? employees
            : employees.Where(e => e.Gender.Equals(genderFilter, StringComparison.OrdinalIgnoreCase));

        Console.WriteLine($"\nEmployees{(string.IsNullOrEmpty(genderFilter) ? "" : $" with Gender '{genderFilter}'")}:");
        foreach (var employee in filteredEmployees)
        {
            Console.WriteLine(employee);
        }
    }

    static Employees ReadEmployeeFromConsole()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Position: ");
        string position = Console.ReadLine();

        Console.Write("Hire Date (yyyy-MM-dd): ");
        DateTime hireDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Salary: ");
        decimal salary = decimal.Parse(Console.ReadLine());

        Console.Write("Gender: ");
        string gender = Console.ReadLine();

        return new Employee
        {
            Name = name,
            Position = position,
            HireDate = hireDate,
            Salary = salary,
            Gender = gender
        };
    }
}
