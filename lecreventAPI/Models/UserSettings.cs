namespace lecreventAPI.Models
{
    public class UserSettings
    {
        public int Id {get; set;}
        public int userId {get; set;} //foreign key
        public int value {get; set;}
        public int rangeStart {get; set;}
        public int rangeEnd {get; set;}
        public User User {get; set;}
    }
}

