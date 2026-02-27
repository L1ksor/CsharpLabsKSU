using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpControlWork1
{
    class Article : IComparable<Article>, IComparer<Article>
    {
        public Person? Author { get; set; }
        public string? Title { get; set; }
        public double Rating { get; set; }

        public Article (Person avtor, string title, double rating)
        {
            this.Author = avtor;
            this.Title = title;
            this.Rating = rating;
        }

        public Article ()
        {
            Author = default(Person);
            Title = default(string);
            Rating = default(double);
        }

        public override string ToString()
        {
            return $"Заголовок статьи - {Title}\n" +
                $"Рейтинг - {Rating}\n" +
                $"Автор - {Author} \n";
        }

        public int CompareTo(Article? other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("null Article");
            }

            return string.Compare(this.Title, other.Title);
        }

        public int Compare(Article? x, Article? y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentNullException("null Article");
            }

            if (x.Author == null || y.Author == null)
            {
                throw new ArgumentNullException("null Article.Author");
            }


            return string.Compare(x.Author.LastName, y.Author.LastName);
        }
    }

}
