class Movie
{
    public int Key { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public string Operator { get; set; }
    public string Genre { get; set; }
    public string Studio { get; set; }
    public string[] Actors { get; set; }

    public Movie(int key, string title, string director, string @operator, string genre, string studio, params string[] actors)
    {
        Key = key;
        Title = title;
        Director = director;
        Operator = @operator;
        Genre = genre;
        Studio = studio;
        Actors = actors;
    }
}