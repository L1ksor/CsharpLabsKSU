namespace CsharpLab6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Иван", Age = 25, Department = "IT", Salary = 60000 },
                new Employee { Id = 2, Name = "Петр", Age = 32, Department = "IT", Salary = 80000 },
                new Employee { Id = 3, Name = "Мария", Age = 28, Department = "HR", Salary = 55000 },
                new Employee { Id = 4, Name = "Анна", Age = 35, Department = "HR", Salary = 65000 },
                new Employee { Id = 5, Name = "Сергей", Age = 45, Department = "Финансы", Salary = 90000 },
                new Employee { Id = 6, Name = "Ольга", Age = 29, Department = "Финансы", Salary = 70000 },
                new Employee { Id = 7, Name = "Алексей", Age = 31, Department = "IT", Salary = 75000 }
            };

            Console.WriteLine("1.Where");
            var itEmployees = employees.Where(e => e.Department == "IT");
            PrintResults(itEmployees);

            Console.WriteLine("\n2.Select:");
            var namesOnly = employees.Select(e => new { e.Name, e.Age });
            foreach (var item in namesOnly)
                Console.WriteLine($"{item.Name}, {item.Age} лет");

            Console.WriteLine("\n3.GroupBy");
            var byDept = employees.GroupBy(e => e.Department);
            foreach (var group in byDept)
            {
                Console.WriteLine($"{group.Key} ({group.Count()} чел.):");
                foreach (var emp in group)
                    Console.WriteLine($"  {emp.Name}");
            }

            Console.WriteLine("\n4. ToArray и ToList:");
            Employee[] array = employees.ToArray();
            List<Employee> list = employees.ToList();
            Console.WriteLine($"Массив: {array.Length} элементов");
            Console.WriteLine($"Список: {list.Count} элементов");

            Console.WriteLine("\n5. Take и Skip:");
            var first3 = employees.Take(3);
            Console.WriteLine("первые 3:");
            PrintResults(first3);

            var skip2 = employees.Skip(2).Take(2);
            Console.WriteLine("пропустить 2, взять 2:");
            PrintResults(skip2);

            Console.WriteLine("\n6.OrderBy:");
            var sorted = employees.OrderBy(e => e.Age);
            PrintResults(sorted);

            Console.WriteLine("\n7. Any и All:");
            bool anyIT = employees.Any(e => e.Department == "IT");
            bool allRich = employees.All(e => e.Salary > 40000);
            Console.WriteLine($"Есть IT сотрудники: {anyIT}");
            Console.WriteLine($"Все с зарплатой > 40000: {allRich}");

            Console.WriteLine("\n8. First и FirstOrDefault:");
            var firstIT = employees.First(e => e.Department == "IT");
            Console.WriteLine($"Первый IT: {firstIT.Name}");

            var firstMarketing = employees.FirstOrDefault(e => e.Department == "Маркетинг");
            Console.WriteLine($"Первый маркетолог: {firstMarketing?.Name ?? "не найден"}");

        static void PrintResults(IEnumerable<Employee> employees)
        {
            foreach (var emp in employees)
                Console.WriteLine($"  {emp}");
        }
    }

    }
}
