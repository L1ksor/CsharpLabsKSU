namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("=== Тестирование DynamicArray ===\n");

            // Тест 1: Обычные операции
            Console.WriteLine("1. Обычные операции (Add, Remove):");
            var array = new DynamicArray<int>();
            array.Add(1);
            array.Add(2);
            array.Add(3);

            Console.WriteLine("До удаления: " + string.Join(", ", array));
            array.Remove(2);
            Console.WriteLine("После удаления 2: " + string.Join(", ", array));
            Console.WriteLine($"Capacity: {array.Capacity}, Count: {array.Count}");

            // Тест 2: Массив с capacity = 0 + Add
            Console.WriteLine("\n2. Массив с начальной емкостью 0 + Add:");
            var array2 = new DynamicArray<int>(0);
            array2.Add(10);
            Console.WriteLine("Элементы: " + string.Join(", ", array2));
            Console.WriteLine($"После добавления элемента: Capacity: {array2.Capacity}");

            // Тест 3: Массив с capacity = 0 + Insert
            Console.WriteLine("\n3. Вставка в массив с capacity = 0:");
            var array3 = new DynamicArray<int>(0);
            array3.Insert(100, 0);
            Console.WriteLine("Элементы: " + string.Join(", ", array3));
            Console.WriteLine($"Capacity: {array3.Capacity}, Count: {array3.Count}");

            // Тест 4: AddRange в непустой массив
            Console.WriteLine("\n4. AddRange в непустой массив:");
            var array4 = new DynamicArray<int>();
            array4.Add(100);
            array4.Add(200);
            Console.WriteLine($"Перед AddRange: {string.Join(", ", array4)}");
            array4.AddRange(new[] { 300, 400, 500 });  // Исправлено: array4
            Console.WriteLine($"После AddRange: {string.Join(", ", array4)}");
            Console.WriteLine($"Capacity: {array4.Capacity}, Count: {array4.Count}");

            // Тест 5: AddRange в массив с capacity = 0
            Console.WriteLine("\n5. AddRange в массив с capacity = 0:");
            var array5 = new DynamicArray<int>(0);
            array5.AddRange(new[] { 10, 20, 30 });  // Исправлено: array5
            Console.WriteLine($"Элементы: {string.Join(", ", array5)}");
            Console.WriteLine($"Capacity: {array5.Capacity}, Count: {array5.Count}");

            // Тест 6: Добавление в уже заполненный массив с capacity = 0
            Console.WriteLine("\n6. Добавление в массив после AddRange:");
            array5.Add(40);  // Добавляем еще один элемент
            array5.Add(50);
            Console.WriteLine($"После добавления 40 и 50: {string.Join(", ", array5)}");
            Console.WriteLine($"Capacity: {array5.Capacity}, Count: {array5.Count}");

            // Тест 7: Многократный AddRange
            Console.WriteLine("\n7. Многократный AddRange:");
            var array6 = new DynamicArray<int>();
            array6.AddRange(new[] { 1, 2 });
            array6.AddRange(new[] { 3, 4, 5 });
            array6.AddRange(new[] { 6, 7, 8, 9, 10 });
            Console.WriteLine($"Элементы: {string.Join(", ", array6)}");
            Console.WriteLine($"Capacity: {array6.Capacity}, Count: {array6.Count}");

            // Тест 8: Конструктор с коллекцией
            Console.WriteLine("\n8. Конструктор с коллекцией:");
            var list = new List<int> { 5, 10, 15, 20 };
            var array7 = new DynamicArray<int>(list);
            Console.WriteLine($"Создан из List: {string.Join(", ", array7)}");
            Console.WriteLine($"Capacity: {array7.Capacity}, Count: {array7.Count}");

            // Создаем тестовые массивы для сравнения
            var compareArray1 = new DynamicArray<int>();
            compareArray1.AddRange(new[] { 1, 2, 3, 4, 5 });

            var compareArray2 = new DynamicArray<int>();
            compareArray2.AddRange(new[] { 1, 2, 3, 4, 5 });

            var compareArray3 = new DynamicArray<int>();
            compareArray3.AddRange(new[] { 1, 2, 3, 4 });

            // Тест 9: Сравнение с другим экземпляром того же класса
            Console.WriteLine("\n9. Сравнение с другим DynamicArray<int>:");
            Console.WriteLine($"compareArray1.Equals(compareArray2): {compareArray1.Equals(compareArray2)}"); // true
            Console.WriteLine($"compareArray1.Equals(compareArray3): {compareArray1.Equals(compareArray3)}"); // false
            // Тест 10: Сравнение с экземпляром другого класса (List<T>)
            Console.WriteLine("\n10. Сравнение с List<int>:");
            List<int> compareList1 = new List<int> { 1, 2, 3, 4, 5 };
            List<int> compareList2 = new List<int> { 1, 2, 3 };

            Console.WriteLine($"compareArray1.Equals(compareList1): {compareArray1.Equals(compareList1)}"); // true
            Console.WriteLine($"compareArray1.Equals(compareList2): {compareArray1.Equals(compareList2)}"); // false
        }
    }
}
