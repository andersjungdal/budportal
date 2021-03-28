namespace ModelsInterfaces.LockData.RawBidLogged
{
    public interface IRawBidCell : ICell
    {
        double Quantity { get; set; }
        double Prize { get; set; }
    }
}