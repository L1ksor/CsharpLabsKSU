using System.Text.Json;
using System.Text.Json.Serialization;

namespace CsharpLab5
{
    [Serializable]
    class Recipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Station CraftingStation { get; set; }
        public int Level { get; set; }
        public Dictionary<string, int> Ingredients { get; set; }

        public Recipe()
        {
            Ingredients = new Dictionary<string, int>();
        }

        public Recipe(string name, string description, int level, Station station)
        {
            Name = name;
            Description = description;
            Level = level;
            CraftingStation = station;
            Ingredients = new Dictionary<string, int>();
        }

    }
}
