namespace NyWine.Wines
{
    public class WineRemoved
    {
        public int WineRemovedId { get; set; }
        public Wine Wine { get; set; }
        public int WineId { get; set; }
        public DateTime RemovedDate { get; set; }
    }
}