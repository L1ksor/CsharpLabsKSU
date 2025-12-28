using System.Text.Json;

namespace CsharpLab5
{
    internal class Program
    {

        static string filePath = "recipes.json";

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
            recipes.Add(newRecipe);
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
            for (int i = 0; i < recipes.Count; i++)
            {
                Recipe recipe = recipes[i];

                Console.WriteLine($"РЕЦЕПТ #{i + 1}");
                Console.WriteLine($"Название: {recipe.Name}");
                Console.WriteLine($"Описание: {recipe.Description}");
                Console.WriteLine($"Станция: {recipe.CraftingStation}");
                Console.WriteLine($"Требуемый уровень: {recipe.Level}");

                Console.WriteLine("Ингредиенты:");
                if (recipe.Ingredients != null && recipe.Ingredients.Count > 0)
                {
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        Console.WriteLine($"  - {ingredient.Key}: {ingredient.Value} шт.");
                    }
                }
                else
                {
                    Console.WriteLine("  (нет ингредиентов)");
                }

                Console.WriteLine("-----------------------------------");
            }
        }

        static public void SaveRecipes(List<Recipe> recipes)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
            };

            string json = JsonSerializer.Serialize(recipes, options);

            File.WriteAllText(filePath, json);

            Console.WriteLine($"\n✓ Успешно сохранено {recipes.Count} рецептов в файл: {filePath}");
            Console.WriteLine($"Файл сохранен по пути: {Path.GetFullPath(filePath)}");
        }
    }
}
