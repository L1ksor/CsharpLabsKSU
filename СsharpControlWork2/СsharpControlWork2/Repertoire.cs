class Repertoire
{
    public int CinemaKey { get; set; }
    public int MovieKey { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public double Price { get; set; }
    public int FreeSeats { get; set; }

    public Repertoire(int cinemaKey, int movieKey, string date, string time, double price, int freeSeats)
    {
        CinemaKey = cinemaKey;
        MovieKey = movieKey;
        Date = date;
        Time = time;
        Price = price;
        FreeSeats = freeSeats;
    }
}