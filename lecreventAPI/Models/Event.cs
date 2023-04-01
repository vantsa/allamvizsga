namespace lecreventAPI.Models;

public class Event
{
    public int Id { get; set;} 
    public int userId {get; set;} //foreign key
    public string description { get; set;} = string.Empty;
    public int spaces {get; set;}
    public DateTime date {get; set;}
    public string time {get; set;}
    public double latitude {get; set;}
    public double longitude {get; set;}
    public virtual User User {get; set;}
}