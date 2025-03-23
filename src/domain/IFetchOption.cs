namespace domain
{
    public interface IFetchOption : IOption
    {
        bool Username { get; }
        bool Password { get; }
    }
}
