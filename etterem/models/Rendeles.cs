namespace etterem.models
{
    public class Rendeles
    {
        public int id { get; set; }
        public string? dish { get; set; }
        public string? description { get; set; }
        public DateTime orderTime { get; set; }
        public DateTime updateTime { get; set; }
        public int vendegId { get; set; }


    }
}
