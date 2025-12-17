using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpLab4
{
    public delegate void SortingCompletedDelegate(string message);

    class SorterAdvanced
    {
        // ЭВЕНТ С МОИМ ДЕЛЕГАТОМ, КОТОРЫЙ ПРИНИМИАЕТ СТРОКУ-СООБЩЕНИЕ. 
        public event SortingCompletedDelegate SortingCompleted;

        public void SortCollection<T>(T[] array, Func<T, T, int> compare)
        {
            string message;

            if (array == null || array.Length <= 1)
            {
                message = "ggWP";
                OnSortingCompleted(message);
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

            message = $"Сортировка завершена. Время: {DateTime.Now}";
            OnSortingCompleted(message);
        }

        private void OnSortingCompleted(string message)
        {
            // ПЕРЕДАЁМ СООБЩЕНИЕ ВСЕМ ПОДПИСЧИКАМ!!!!!!!!
            SortingCompleted?.Invoke(message);
        }

        private void Swap<T>(T[] array, int i, int j)
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
        
    }
}
