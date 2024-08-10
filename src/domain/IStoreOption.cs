namespace domain
{
    public interface IStoreOption : IOption
    {
        string Username { get; }
        string Password { get; }
    }
}
