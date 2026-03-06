using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpControlWork1
{
    delegate TKey KeySelector<TKey>(Magazine mg);

    class MagazineCollection<TKey> 
    {
        Dictionary<TKey, Magazine> _dictMagazine = new();
        KeySelector<TKey> _keySelector;

        public double MaxAverageRating
        {
            get
            {
                return _dictMagazine.Values.Max(m => m.AverageRatingValue);
            }
        }

        public MagazineCollection(KeySelector<TKey> keySelector)
        {
            _keySelector = keySelector;
        }

        public void AddDefaults()
        {
            Magazine mg = new Magazine($"Журнал 0", Frequency.Monthly, DateTime.Now, 1000);
            _dictMagazine.Add( _keySelector(mg) ,mg);
        }

        public void AddMagazines(params Magazine[] magazines) 
        {
            foreach (var magazine in magazines)
            {
                _dictMagazine.Add(_keySelector(magazine), magazine);
            }
        }

        public IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency value)
        {
            return _dictMagazine.Where(pair => pair.Value.Periodicity == value);
        }

        public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> GroupByFrequency
        {
            get
            {
                return _dictMagazine.GroupBy(pair => pair.Value.Periodicity);
            }
        }

        public override string ToString() 
        {
            string result = "Коллекция магазина:\n";
            foreach (var pair in _dictMagazine)
            {
                result += $"Ключ: {pair.Key} значение {pair.Value}\n";
            }
            return result;
        }

        public string ToShortString() 
        {
            string result = "Коллекция магазина(кратко):\n";
            foreach (var pair in _dictMagazine)
            {
                result += $"Ключ: {pair.Key}, Название: {pair.Value.TitleJournal}, Средний рейтинг: {pair.Value.AverageRatingValue}\n";
            }
            return result;
        } 
    }
}

