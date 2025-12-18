using System.Text.Json;

namespace CsharpLab5
{
    internal class Program
    {

        static string filePath = " ";

        static void Main(string[] args)
        {

            List<Recipe> recipes = LoadRecipes();

            Console.WriteLine("Создание рецепта для RPG SUPERGAME");
            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. показать все рецепты");
                Console.WriteLine("2. добавить новый рецепт");
                Console.WriteLine("0. выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowRecipes(recipes);
                        break;
                    case "2":
                        AddNewRecipe(recipes);
                        SaveRecipes(recipes);
                        break;
                    case "0":
                        return;
                }
            }
        }
        static List<Recipe> LoadRecipes()
        {
            if (!File.Exists(filePath))
            {
                return new List<Recipe>();
            }

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Recipe>>(json) ?? new List<Recipe>();
        }

        static public void AddNewRecipe (List<Recipe> recipes)
        {
            Console.Write("Введите название рецепта: ");
            string name = Console.ReadLine();

            Console.Write("Введите описание: ");
            string description = Console.ReadLine();

            Console.Write("Выберите станцию (Workbench, Anvil, AlchemyTable): ");
            string stationInput = Console.ReadLine();
            Station craftingStation = Enum.Parse<Station>(stationInput);

            Console.Write("Введите требуемый уровень: ");
            int level = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите ингредиенты (название и количество). Для завершения введите пустое название.");

            Dictionary<string, int> ingredients = AddIngredients();

            Recipe newRecipe = new Recipe(name, description, level, craftingStation)
            {
                Ingredients = ingredients
            };

        }

        private static Dictionary<string, int> AddIngredients()
        {
            var ingredients = new Dictionary<string, int>();

            while (true)
            {
                Console.Write("Название ингредиента: ");
                string ingName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingName))
                {
                    break; // Выход из цикла
                }

                Console.Write($"Количество для {ingName}: ");
                if (int.TryParse(Console.ReadLine(), out int count))
                {
                    ingredients.Add(ingName, count);
                }
            }

            return ingredients;
        }

        static public void ShowRecipes(List<Recipe> recipes)
        {

        }

        static public void SaveRecipes(List<Recipe> recipes)
        {

        }
    }
}
