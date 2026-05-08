using System.Text;

namespace СsharpControlWork2
{
    internal class Program
    {
        private static List<Cinema> cinemas = new();
        private static List<Movie> movies = new();
        private static List<Repertoire> repertoires = new();

        static string[] paths =
        {
            @"..\..\..\Cinemas.txt",
            @"..\..\..\Movies.txt",
            @"..\..\..\Repertoire.txt"
        };

        static void Main(string[] args)
        {
            // Загрузка всех трёх файлов
            LoadCinemas(paths[0]);
            LoadMovies(paths[1]);
            LoadRepertoires(paths[2]);

            int choice = -1;
            while (true)
            {
                ShowMenu();
                
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1: Query_RepertoireByCinema(); break;
                    case 2: Query_ActionMoviesCinemas(); break;
                    case 3: Query_FreeSeatsBySession(); break;
                    case 4: Query_TicketPrice(); break;
                    case 5: Query_MoviesByDirector(); break;
                    case 6: Query_ComedyCinemas(); break;
                    case 7: AddMovie(); break;
                    case 8: RemoveMovie(); break;
                    case 9:
                        Console.Write("Выйти из программы? (да/нет): ");
                        if (Console.ReadLine() == "да")
                            Environment.Exit(0);
                        else Console.Clear();
                        continue;
                    default:
                        Console.WriteLine("Неверный номер. Нажмите Enter...");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                }
            }
        }

        static void LoadCinemas(string path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                // [0]=Key, [1]=Name, [2]=Address, [3]=Category,
                // [4]=Seats, [5]=Halls, [6]=Status
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    cinemas.Add(new Cinema(
                        Convert.ToInt32(parts[0]),
                        parts[1],
                        parts[2],
                        parts[3],
                        Convert.ToInt32(parts[4]),
                        Convert.ToInt32(parts[5]),
                        parts[6]
                    ));
                }
            }
        }

        static void LoadMovies(string path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    // [0]=Key, [1]=Title, [2]=Director, [3]=Operator,
                    // [4]=Genre, [5]=Studio, остальное — актеры
                    string[] actors = new string[parts.Length - 6];
                    Array.Copy(parts, 6, actors, 0, parts.Length - 6);
                    movies.Add(new Movie(
                        Convert.ToInt32(parts[0]),
                        parts[1],
                        parts[2],
                        parts[3],
                        parts[4],
                        parts[5],
                        actors
                    ));
                }
            }
        }

        static void LoadRepertoires(string path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    // [0]=CinemaKey, [1]=MovieKey, [2]=Date,
                    // [3]=Time, [4]=Price, [5]=FreeSeats

                    repertoires.Add(new Repertoire(
                        Convert.ToInt32(parts[0]),
                        Convert.ToInt32(parts[1]),
                        parts[2],
                        parts[3],
                        Convert.ToDouble(parts[4]),
                        Convert.ToInt32(parts[5])
                    ));
                }
            }
        }


        static void ShowMenu()
        {
            Console.WriteLine("\tВыберите запрос:\n");
            Console.WriteLine("1 - Репертуар кинотеатра");
            Console.WriteLine("2 - В каких кинотеатрах можно посмотреть боевики");
            Console.WriteLine("3 - Число свободных мест на данный сеанс в заданном кинотеатре");
            Console.WriteLine("4 - Цена билетов на заданный сеанс в указанном кинотеатре");
            Console.WriteLine("5 - Фильмы заданного режиссера и в каких кинотеатрах они идут");
            Console.WriteLine("6 - В каких кинотеатрах демонстрируются комедии");
            Console.WriteLine("7 - Добавить фильм");
            Console.WriteLine("8 - Удалить фильм");
            Console.WriteLine("\n9 - Выход из программы");
            Console.Write("\nВаш выбор: ");
        }

        static void Query_RepertoireByCinema()
        {
            Console.Clear();
            Console.Write("Введите название кинотеатра: ");
            string cinemaName = Console.ReadLine();

            var query = repertoires
                .Join(cinemas,
                    r => r.CinemaKey,
                    c => c.Key,
                    (r, c) => new { r, c })
                .Join(movies,
                    rc => rc.r.MovieKey,
                    m => m.Key,
                    (rc, m) => new { rc.c, rc.r, m })
                .Where(x => x.c.Name.ToLower() == cinemaName.ToLower())
                .Select(x => new
                {
                    x.c.Name,
                    x.m.Title,
                    x.m.Genre,
                    x.r.Date,
                    x.r.Time,
                    x.r.Price,
                    x.r.FreeSeats
                });

            Console.WriteLine($"\nРепертуар кинотеатра \"{cinemaName}\":");
            Console.WriteLine("{0,-20} {1,-15} {2,-10} {3,-12} {4,-10} {5}",
                "Название", "Жанр", "Дата", "Время", "Цена", "Свободных мест");
            Console.WriteLine(new string('-', 80));

            foreach (var item in query)
            {
                Console.WriteLine("{0,-20} {1,-15} {2,-10} {3,-12} {4,-10} {5}",
                    item.Title, item.Genre, item.Date, item.Time, item.Price, item.FreeSeats);
            }

            Pause();
        }

        static void Query_ActionMoviesCinemas()
        {
            Console.Clear();

            var query = repertoires
                .Join(movies.Where(m => m.Genre.ToLower() == "боевик"),
                    r => r.MovieKey,
                    m => m.Key,
                    (r, m) => new { r, m })
                .Join(cinemas,
                    rm => rm.r.CinemaKey,
                    c => c.Key,
                    (rm, c) => new { c.Name, c.Address, rm.m.Title, rm.m.Genre, rm.r.Date, rm.r.Time })
                .Distinct();

            Console.WriteLine("Кинотеатры, где идут боевики:\n");
            Console.WriteLine("{0,-20} {1,-25} {2,-20} {3,-12} {4}",
                "Кинотеатр", "Адрес", "Фильм", "Дата", "Время");
            Console.WriteLine(new string('-', 90));

            foreach (var item in query)
            {
                Console.WriteLine("{0,-20} {1,-25} {2,-20} {3,-12} {4}",
                    item.Name, item.Address, item.Title, item.Date, item.Time);
            }

            Pause();
        }

        static void Query_FreeSeatsBySession()
        {
            Console.Clear();
            Console.Write("Введите название кинотеатра: ");
            string cinemaName = Console.ReadLine();
            Console.Write("Введите дату (ДД.ММ.ГГГГ): ");
            string date = Console.ReadLine();
            Console.Write("Введите время (ЧЧ:ММ): ");
            string time = Console.ReadLine();

            var query = repertoires
                .Join(cinemas.Where(c => c.Name.ToLower() == cinemaName.ToLower()),
                    r => r.CinemaKey,
                    c => c.Key,
                    (r, c) => new { r, c })
                .Join(movies,
                    rc => rc.r.MovieKey,
                    m => m.Key,
                    (rc, m) => new { rc.c.Name, m.Title, rc.r.Date, rc.r.Time, rc.r.FreeSeats })
                .Where(x => x.Date == date && x.Time == time);

            Console.WriteLine($"\nСвободные места в \"{cinemaName}\" на {date} в {time}:");
            foreach (var item in query)
            {
                Console.WriteLine($"Фильм: {item.Title}, Свободных мест: {item.FreeSeats}");
            }

            if (!query.Any())
                Console.WriteLine("Сеанс не найден.");

            Pause();
        }

        static void Query_TicketPrice()
        {
            Console.Clear();
            Console.Write("Введите название кинотеатра: ");
            string cinemaName = Console.ReadLine();
            Console.Write("Введите название фильма: ");
            string movieTitle = Console.ReadLine();
            Console.Write("Введите дату (ДД.ММ.ГГГГ): ");
            string date = Console.ReadLine();
            Console.Write("Введите время (ЧЧ:ММ): ");
            string time = Console.ReadLine();

            var query = repertoires
                .Join(cinemas.Where(c => c.Name.ToLower() == cinemaName.ToLower()),
                    r => r.CinemaKey, c => c.Key, (r, c) => new { r, c })
                .Join(movies.Where(m => m.Title.ToLower() == movieTitle.ToLower()),
                    rc => rc.r.MovieKey, m => m.Key, (rc, m) => new { rc.c, m, rc.r })
                .Where(x => x.r.Date == date && x.r.Time == time)
                .Select(x => new { x.m.Title, x.r.Price, x.c.Name, x.c.Category });

            Console.WriteLine($"\nЦена билета на \"{movieTitle}\" в \"{cinemaName}\":");
            foreach (var item in query)
            {
                Console.WriteLine($"Категория: {item.Category}, Цена: {item.Price} руб.");
            }

            if (!query.Any())
                Console.WriteLine("Сеанс не найден.");

            Pause();
        }

        static void Query_MoviesByDirector()
        {
            Console.Clear();
            Console.Write("Введите фамилию режиссёра: ");
            string director = Console.ReadLine();

            var query = repertoires
                .Join(movies.Where(m => m.Director.ToLower().Contains(director.ToLower())),
                    r => r.MovieKey, m => m.Key, (r, m) => new { r, m })
                .Join(cinemas,
                    rm => rm.r.CinemaKey, c => c.Key, (rm, c) => new
                    {
                        rm.m.Title,
                        rm.m.Director,
                        Cinema = c.Name,
                        c.Address,
                        rm.r.Date,
                        rm.r.Time
                    });

            Console.WriteLine($"\nФильмы режиссёра \"{director}\":");
            Console.WriteLine("{0,-25} {1,-20} {2,-10} {3}", "Фильм", "Кинотеатр", "Дата", "Время");
            Console.WriteLine(new string('-', 70));

            foreach (var item in query)
            {
                Console.WriteLine("{0,-25} {1,-20} {2,-10} {3}",
                    item.Title, item.Cinema, item.Date, item.Time);
            }

            if (!query.Any())
                Console.WriteLine("Фильмы не найдены.");

            Pause();
        }

        static void Query_ComedyCinemas()
        {
            Console.Clear();

            var query = repertoires
                .Join(movies.Where(m => m.Genre.ToLower() == "комедия"),
                    r => r.MovieKey, m => m.Key, (r, m) => new { r, m })
                .Join(cinemas,
                    rm => rm.r.CinemaKey, c => c.Key, (rm, c) => new
                    {
                        Cinema = c.Name,
                        c.Address,
                        Movie = rm.m.Title,
                        rm.m.Genre,
                        rm.r.Date,
                        rm.r.Time
                    })
                .Distinct();

            Console.WriteLine("Кинотеатры, где идут комедии:\n");
            Console.WriteLine("{0,-20} {1,-25} {2,-20} {3,-12} {4}",
                "Кинотеатр", "Адрес", "Фильм", "Дата", "Время");
            Console.WriteLine(new string('-', 90));

            foreach (var item in query)
            {
                Console.WriteLine("{0,-20} {1,-25} {2,-20} {3,-12} {4}",
                    item.Cinema, item.Address, item.Movie, item.Date, item.Time);
            }

            Pause();
        }

        static void AddMovie()
        {
            Console.Clear();
            Console.WriteLine("=== Добавление нового фильма ===\n");
            Console.Write("Ключ: ");
            int key = int.Parse(Console.ReadLine());
            Console.Write("Название: ");
            string title = Console.ReadLine();
            Console.Write("Режиссёр: ");
            string director = Console.ReadLine();
            Console.Write("Оператор: ");
            string operatorName = Console.ReadLine();
            Console.Write("Жанр: ");
            string genre = Console.ReadLine();
            Console.Write("Киностудия: ");
            string studio = Console.ReadLine();
            Console.Write("Актёры (через запятую): ");
            string[] actors = Console.ReadLine().Split(',').Select(a => a.Trim()).ToArray();

            movies.Add(new Movie(key, title, director, operatorName, genre, studio, actors));
            Console.WriteLine("\nФильм успешно добавлен!");
            Pause();
        }


        static void RemoveMovie()
        {
            Console.Clear();
            Console.WriteLine("=== Удаление фильма ===\n");
            Console.WriteLine("Список фильмов:");
            foreach (var m in movies)
                Console.WriteLine($"{m.Key}: {m.Title} ({m.Genre})");

            Console.Write("\nВведите ключ фильма для удаления: ");
            int key = int.Parse(Console.ReadLine());
            var movie = movies.FirstOrDefault(m => m.Key == key);

            if (movie != null)
            {
                movies.Remove(movie);
                // Удаляем связанные записи из репертуара
                repertoires.RemoveAll(r => r.MovieKey == key);
                Console.WriteLine($"Фильм \"{movie.Title}\" удалён.");
            }
            else
            {
                Console.WriteLine("Фильм не найден.");
            }

            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
