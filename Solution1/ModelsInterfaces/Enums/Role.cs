namespace ModelsInterfaces.Enums
{
    public enum Role : byte
    {
        NonAuthorized = byte.MinValue,
        Admin = 1,
        Bid = 2,
        Anonymous = byte.MaxValue,
    }
}