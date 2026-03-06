namespace CsharpControlWork1
{
    internal class Program
    {
        static KeyValuePair<Edition, Magazine> GeneratePair(int j)
        {
            Edition key = new Edition($"Издание {j}", DateTime.Now, 1000 + j);
            Magazine value = new Magazine($"Журнал {j}", Frequency.Monthly, DateTime.Now, 5000 + j);
            return new KeyValuePair<Edition, Magazine>(key, value);
        }

        static void Main(string[] args)
        {
            // ========== ЧАСТЬ 1: Работа с Magazine и сортировка статей ==========
            Console.WriteLine("=== ЧАСТЬ 1: Сортировка статей в журнале ===\n");

            Person author1 = new Person("Иван", "Петров", new DateTime(1980, 1, 1));
            Person author2 = new Person("Мария", "Иванова", new DateTime(1985, 3, 15));
            Person author3 = new Person("Петр", "Сидоров", new DateTime(1975, 7, 20));

            Article article1 = new Article(author1, "Основы программирования", 4.5);
            Article article2 = new Article(author2, "Алгоритмы и структуры данных", 4.8);
            Article article3 = new Article(author3, "Базы данных для начинающих", 4.2);
            Article article4 = new Article(author1, "Паттерны проектирования", 4.9);
            Article article5 = new Article(author2, "LINQ для чайников", 4.1);
            Article[] articles = {article1,article2,article3,article4,article5};

            Magazine magazine = new Magazine("Программист", Frequency.Monthly, DateTime.Now, 5000);
            magazine.AddArticles(articles);

            Console.WriteLine("ИСХОДНЫЙ СПИСОК СТАТЕЙ:");
            Console.WriteLine(magazine);

            Console.WriteLine("\n--- СОРТИРОВКА ПО НАЗВАНИЮ ---");
            magazine.SortArticlesByTitle();
            Console.WriteLine(magazine);

            Console.WriteLine("\n--- СОРТИРОВКА ПО ФАМИЛИИ АВТОРА ---");
            magazine.SortArticlesByAuthor();
            Console.WriteLine(magazine);

            Console.WriteLine("\n--- СОРТИРОВКА ПО РЕЙТИНГУ ---");
            magazine.SortArticlesByRating();
            Console.WriteLine(magazine);

            // ========== ЧАСТЬ 2: MagazineCollection<string> ==========
            Console.WriteLine("\n=== ЧАСТЬ 2: MagazineCollection<string> ===\n");

            // Создаем делегат для вычисления ключа (ключ = название журнала)
            KeySelector<string> keySelector = mg => mg.TitleJournal;

            // Создаем коллекцию журналов
            MagazineCollection<string> magazineCollection = new MagazineCollection<string>(keySelector);

            // Создаем несколько разных журналов
            Magazine mag1 = new Magazine("Наука и жизнь", Frequency.Monthly, new DateTime(2023, 1, 1), 10000);
            mag1.AddArticles(article1, article2);

            Magazine mag2 = new Magazine("Техника молодежи", Frequency.Weekly, new DateTime(2023, 2, 1), 5000);
            mag2.AddArticles(article3, article4);

            Magazine mag3 = new Magazine("Квант", Frequency.Yearly, new DateTime(2023, 3, 1), 2000);
            mag3.AddArticles(article5);

            Magazine mag4 = new Magazine("Вокруг света", Frequency.Monthly, new DateTime(2023, 4, 1), 8000);
            mag4.AddArticles(article1, article3, article5);

            // Добавляем журналы в коллекцию
            magazineCollection.AddMagazines(mag1, mag2, mag3, mag4);

            // Выводим коллекцию
            Console.WriteLine("КОЛЛЕКЦИЯ ЖУРНАЛОВ:");
            Console.WriteLine(magazineCollection);

            // ========== ЧАСТЬ 3: Операции с коллекцией ==========
            Console.WriteLine("\n=== ЧАСТЬ 3: Операции с коллекцией ===\n");

            // 1. Максимальный средний рейтинг
            Console.WriteLine($"Максимальный средний рейтинг: {magazineCollection.MaxAverageRating:F2}\n");

            // 2. FrequencyGroup - выбор журналов с заданной периодичностью
            Console.WriteLine("--- ЖУРНАЛЫ С ПЕРИОДИЧНОСТЬЮ Monthly ---");
            var monthlyMagazines = magazineCollection.FrequencyGroup(Frequency.Monthly);
            foreach (var pair in monthlyMagazines)
            {
                Console.WriteLine($"Ключ: {pair.Key}, Название: {pair.Value.TitleJournal}");
            }

            Console.WriteLine("\n--- ЖУРНАЛЫ С ПЕРИОДИЧНОСТЬЮ Weekly ---");
            var weeklyMagazines = magazineCollection.FrequencyGroup(Frequency.Weekly);
            foreach (var pair in weeklyMagazines)
            {
                Console.WriteLine($"Ключ: {pair.Key}, Название: {pair.Value.TitleJournal}");
            }

            // 3. Группировка по периодичности
            Console.WriteLine("\n--- ГРУППИРОВКА ЖУРНАЛОВ ПО ПЕРИОДИЧНОСТИ ---");
            var groups = magazineCollection.GroupByFrequency;

            foreach (var group in groups)
            {
                Console.WriteLine($"\nГруппа: {group.Key}");
                foreach (var pair in group)
                {
                    Console.WriteLine($"  Журнал: {pair.Value.TitleJournal}, Средний рейтинг: {pair.Value.AverageRatingValue:F2}");
                }
            }


            var countTests = int.Parse( Console.ReadLine() );
            var testCol = new TestCollections<Edition, Magazine>(countTests, GeneratePair);
            Console.WriteLine(testCol);
        }

    }
}
