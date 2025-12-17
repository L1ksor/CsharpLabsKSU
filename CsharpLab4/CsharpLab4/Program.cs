namespace CsharpLab4
{
    internal class Program
    {

        public delegate int ComparisonDelegate<T>(T x, T y);

        static void Main(string[] args)
        {
            var ListInts = new int[] { 28,32,23,2,12,32,43,54 };
            var ListString = new string[] { "gd", "bcd", "acd", "d" };

            SortCollection(ListInts, (int a,int b) => a.CompareTo(b));
            SortCollection(ListString, (string a, string b) => a.CompareTo(b));


            foreach (var x in ListString)
                Console.WriteLine(x);
            foreach (var y in ListInts) 
                Console.WriteLine(y);
        }

        

        public static void SortCollection<T>(T[] array, ComparisonDelegate<T> compare)
        {
            if (array == null || array.Length == 1)
            {
                return;
            }

            int j;
            for (int i = 1; i < array.Length; i++)
            {
                j = i;
                while (j > 0 && compare(array[j-1], array[j]) > 0)
                {
                    Swap(array, j, j - 1);
                    j--;
                }

            }
        }

        static void Swap<T>(T[] array, int i, int j)
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;

        }
    }
}
