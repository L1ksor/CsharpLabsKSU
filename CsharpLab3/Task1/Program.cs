namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            var caseInsensitiveDictionary = new Dictionary<string, int>(comparer);

            using (StreamReader sr = new StreamReader("../../../text.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] tokensLine = line.Split(new char[]{' ', ',', '.'});


                    foreach (var token in tokensLine)
                    {
                        if (string.IsNullOrWhiteSpace(token)) continue;

                        if (caseInsensitiveDictionary.ContainsKey(token))
                        {
                            caseInsensitiveDictionary[token]++;
                        }
                        else
                        {
                            caseInsensitiveDictionary.Add(token, 1);
                        }    
                    }
                }
            }

            int summa = caseInsensitiveDictionary.Count;
            foreach (var item in caseInsensitiveDictionary)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
                
            }
            Console.WriteLine(summa);



        }

        
    }
}
