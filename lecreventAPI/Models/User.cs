namespace lecreventAPI.Models
{
    public class User
    {
        public int Id { get; set;} 
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public int age { get; set; }
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;

        internal static object FindFirstValue(string v)
        {
            throw new NotImplementedException();
        }
    }
}

