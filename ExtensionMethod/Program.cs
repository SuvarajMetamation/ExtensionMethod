using System.Net.Mail;
using static System.Console;

#region class Program -----------------------------------------------------------------------------
class Program {
    #region Main Method ---------------------------------------------
    static void Main () {
        //String Extension
        string email = "test@gmail.com";
        string str = "Trumpf".ReverseText ();
        WriteLine (email.IsValidEmail ());
        WriteLine (str);
        WriteLine ();

        //Integer Extension
        int even = 4;
        WriteLine (even.IsEven ());
        WriteLine (144.ReverseSquare ());
        WriteLine ();

        //Inheritance with extension
        var myDog = new Dog ();
        myDog.Speak ();
        myDog.Identify ();
        WriteLine ();

        // Collection Extension
        List<int> numbers = [10, 20, 30, 40, 50];
        string[] arr = ["Trumpf", "Metamation"];
        WriteLine ($"Is Empty: {numbers.IsEmpty ()}");
        WriteLine ($"Printing numbers in List.");
        numbers.PrintItems ();
        WriteLine ("Printing string in array.");
        arr.PrintItems ();
        WriteLine ();

        // Class Extension
        Employee emp = new (1, "John", "IT", 75000.0m, 6);
        WriteLine (emp.IsExperienced ());
        WriteLine (emp.IsHighSalary ());
        List<Employee> empList = [
                     new(1, "John", "IT", 75000.0m, 6),
                     new(2, "David", "HR", 65000.0m, 3),
                     new(3, "Sarah", "Finance", 85000.0m, 8) ];
        WriteLine (empList.GetSummary ());
        WriteLine ();

        // LINQ Extension 
        List<Employee> employees =
        [
            new Employee{ Id = 1,Name = "John",Department = "Development",Salary = 75000,Experience = 7},
            new Employee{Id = 2,Name = "David",Department = "Testing",Salary = 45000,Experience = 3},
            new Employee{Id = 3,Name = "Sam",Department = "Development",Salary = 65000,Experience = 6},
            new Employee{Id = 4,Name = "Alex",Department = "HR",Salary = 40000,Experience = 2}
        ];
        var highestSalary = employees.Where (emp => emp.Salary > 50000).ToList ();
        WriteLine (employees.GetSummary ());
        WriteLine ();
    }
    #endregion
}
#endregion

#region class StringExtension --------------------------------------------------------------------------
public static class StingExtension {
    public static bool IsValidEmail (this string? email) {
        if (string.IsNullOrWhiteSpace (email)) return false;
        try {
            MailAddress addr = new (email);
            return addr.Address == email;
        } catch {
            return false;
        }
    }

    public static string ReverseText (this string value) => new ([.. value.Reverse ()]);
}
#endregion

#region class IntegerExtension -------------------------------------------------------------------------
public static class IntegerExtension {
    public static bool IsEven (this int value) => value % 2 == 0;

    public static int ReverseSquare (this int value) {
        if (value < 0) ArgumentException.ThrowIfNullOrEmpty ("Number cannot be negative(-).");
        if (value < 2) return value;
        long x = value;
        while (x > value / x) x = (x + value / x) / 2;
        return (int)x;
    }
}
#endregion

#region class CollectionExtension -----------------------------------------------------------------------
public static class CollectionExtension {
    public static bool IsEmpty<T> (this IEnumerable<T> collection) => !collection.Any ();

    public static void PrintItems<T> (this IEnumerable<T> collection) {
        foreach (T? item in collection) WriteLine (item);
    }
}
#endregion

#region InheritanceExtensions --------------------------------------------------------------------
public class Animal {
    public virtual void Speak () => WriteLine ("Animal speaks");
}

public class Dog : Animal {
    public override void Speak () => WriteLine ("Dog barks");
}

public static class AnimalExtension {
    public static void Identify (this Animal animal) => WriteLine ("I am an Animal extension");

    public static void Identify (this Dog dog) => WriteLine ("I am a Dog extension");
}
#endregion

#region CustomizeClassExtension ---------------------------------------------------------------------------
public class Employee {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public int Experience { get; set; }

    #region Parameterized Constructor -------------------------------
    public Employee (int id, string name, string dept, decimal salary, int exp) {
        Id = id;
        Name = name;
        Department = dept;
        Salary = salary;
        Experience = exp;
    }
    #endregion

    #region Non-Parameterized Constructor ---------------------------
    public Employee () { }
    #endregion
}

public static class EmployeeExtension {
    public static bool IsExperienced (this Employee emp) => emp.Experience >= 5;

    public static bool IsHighSalary (this Employee emp) => emp.Salary > 50000;

    public static string GetSummary (this List<Employee> employees) =>
    string.Join (Environment.NewLine, employees.Select (emp =>
            $"{emp.Name} | {emp.Department} | " +
            $"Salary: {emp.Salary} | " +
            $"Experience: {emp.Experience} years"));
}
#endregion
