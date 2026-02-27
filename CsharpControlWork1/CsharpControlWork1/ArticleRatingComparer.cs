namespace CsharpControlWork1
{
    class ArticleRatingComparer : IComparer<Article>
    {
        public int Compare(Article? x, Article? y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentNullException("null Article");
            }

            if (x.Rating < y.Rating) return -1;
            if (x.Rating > y.Rating) return 1;
            return 0;
        }
    }

}
