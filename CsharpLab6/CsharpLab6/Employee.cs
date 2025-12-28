namespace CsharpLab6
{
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name} ({Age} лет), {Department}, {Salary:C}";
        }
    }
}
