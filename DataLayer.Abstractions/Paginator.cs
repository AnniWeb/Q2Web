namespace DataLayer.Abstractions
{
    public class Paginator
    {
        public int Limit { get; set; } = 10;
        public int Offset { get; set; } = 0;
    }
}