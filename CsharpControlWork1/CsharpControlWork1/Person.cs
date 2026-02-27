namespace CsharpControlWork1
{
    public class Person
    {
        string _firstName;
        string _lastName;
        DateTime _birthday;

        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public DateTime Birthday { get => _birthday; set => _birthday = value; }

        public Person(string firstName, string lastName, DateTime birthday)
        {
            FirstName = firstName;
            _lastName = lastName;
            _birthday = birthday;
        }

       
    }
}