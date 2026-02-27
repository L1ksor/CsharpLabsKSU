using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CsharpControlWork1
{


    class Magazine
    {
        private string _title;
        private Frequency _periodicity;
        private DateTime _release;
        int _pressrun;
        private List<Article>? _articlesJournal;
        private List<Person> _editors;

        public string TitleJournal
        {
            get => _title; set => _title = value;
        }

        public Frequency Periodicity
        {
            get => _periodicity; set => _periodicity = value;
        }

        public DateTime ReleaseDate
        {
            get => _release;  set => _release = value;
        }

        public int Pressrun
        { 
            get => _pressrun; set => _pressrun = value;
        }

        public List<Article> ArticlesJournal
        { 
            get => _articlesJournal; set => _articlesJournal = value;
        }


        private bool this[Frequency index]
        {
            get
            {
                return _periodicity == index;
            }
        }

        public double AverageRatingValue
        {
            get
            {
                if (_articlesJournal == null || _articlesJournal.Count == 0)
                    return 0;

                double sum = 0;
                foreach (var article in _articlesJournal)
                {
                    sum += article.Rating;
                }
                return sum / _articlesJournal.Count;
            }
        }

        public Magazine(string title, Frequency periodicity, DateTime release, int pressrun) 
        { 
            _title = title;
            _periodicity = periodicity; 
            _release = release;
            _pressrun = pressrun;
            _articlesJournal = new List<Article>();    
            _editors = new List<Person>(); ;
        }

        public Magazine()
        {
            _title = default(string);
            _periodicity = default(Frequency);
            _release = default(DateTime);
            _pressrun = default(int);
            _articlesJournal = new List<Article>();
            _editors = new List<Person>();
        }

        private void AddArticles (params Article[] articles)
        {
            _articlesJournal.AddRange(articles);
        }

        public void SortArticlesByTitle()
        {
            _articlesJournal.Sort();
        }

        public void SortArticlesByAuthor()
        {
            Article comparer = new Article();
            _articlesJournal.Sort(comparer);
        }

        public void SortArticlesByRating()
        {
            var comparer = new ArticleRatingComparer();
            _articlesJournal.Sort(comparer);
        }

        public override string ToString()
        {
            string result = $"название - {_title}\n" +
                $"Периодичность - {_periodicity}\n" +
                $"Дата выпуска - {_release}\n" +
                $"Тираж - {_pressrun}\n" +
                $"Cписок статей:" ;

            foreach (var article in _articlesJournal)
            {
                result += article.ToString();
            }

            return result;
        }

        public virtual string ToShortString()
        {
            string result = $"название - {_title}\n" +
                $"Периодичность - {_periodicity}\n" +
                $"Дата выпуска - {_release}\n" +
                $"Тираж - {_pressrun}\n" +
                $"Срдений рейтинг журнала - {AverageRatingValue}";

            return result ;
        }
    }
}
