namespace CsharpLab4
{
    static class Sorter
    {

        //public delegate int Comparison<T>(T x, T y);


        static public void SortCollection<T>(T[] array, Func<T, T, int> compare)
        {
            if (array == null || array.Length <= 1)
            {
                return;
            }

            int j;
            for (int i = 1; i < array.Length; i++)
            {
                j = i;
                while (j > 0 && compare(array[j - 1], array[j]) > 0)
                {
                    Swap(array, j, j - 1);
                    j--;
                }

            }
        }

        static private void Swap<T>(T[] array, int i, int j)
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
