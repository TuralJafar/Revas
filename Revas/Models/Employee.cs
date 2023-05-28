namespace Revas.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Facebook { get; set; }    
        public string LinkEdin { get; set; }
        public string Google { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
