class Cinema
{
    public int Key { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Category { get; set; }
    public int Seats { get; set; }
    public int Halls { get; set; }
    public string Status { get; set; }

    public Cinema(int key, string name, string address, string category, int seats, int halls, string status)
    {
        Key = key;
        Name = name;
        Address = address;
        Category = category;
        Seats = seats;
        Halls = halls;
        Status = status;
    }
}