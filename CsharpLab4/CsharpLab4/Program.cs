using Microsoft.VisualBasic;

namespace CsharpLab4
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var ints = new int[] { 28,32,23,2,12,32,43,54 };
            var strings = new string[] { "gd", "bcd", "acd", "d" };

            Sorter.SortCollection(ints, (int a,int b) => a.CompareTo(b));
            Sorter.SortCollection(strings, (string a, string b) => a.CompareTo(b));


            foreach (var x in strings)
                Console.WriteLine(x);
            foreach (var y in ints) 
                Console.WriteLine(y);

            var sorter = new SorterAdvanced();

            // Подписываемся - передаем только string!
            sorter.SortingCompleted += OnSortingCompleted;

            // Лямбда еще проще:
            sorter.SortingCompleted += message =>
                Console.WriteLine($"Лямбда получила: {message}");

            int[] numbers = { 5, 2, 8, 1, 9 };
            Console.WriteLine($"Сортируем: {string.Join(", ", numbers)}");

            sorter.SortCollection(numbers, (a, b) => a.CompareTo(b));

            Console.WriteLine($"Результат: {string.Join(", ", numbers)}");

        }

        // Обработчик получает ТОЛЬКО сообщение, а не object sender!
        static void OnSortingCompleted(string message)
        {
            Console.WriteLine($"Событие: {message}");
        }
    }
}
