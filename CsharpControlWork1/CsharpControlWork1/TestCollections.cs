using System.Runtime.CompilerServices;

namespace CsharpControlWork1
{
    delegate void KeyValuePair<Tkey, Tvalue>();
    delegate void GenerateElement <Tkey, Tvalue> ();
    
    public class TestCollections<Tkey, Tvalue>
    {
        List<Tkey> listKeys;
        List<Tvalue> listValues;
        Dictionary<Tkey, Tvalue> dict;
        Dictionary<string, Tvalue> dictString;
        GenerateElement<Tkey, Tvalue> generateElement;

        public TestCollections()
        {
            listKeys = new List<Tkey> ();
            listValues = new List<Tvalue> ();
            dict = new Dictionary<Tkey, Tvalue>();
            dictString = new Dictionary<string, Tvalue> (); 


        }

        public GeneratorEl(int number, GenerateElement<Tkey, Tvalue> generation)
        {
            for (int i = 0; i < number; i++)
            {

            }
        }


    }
}
