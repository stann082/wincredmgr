namespace domain
{
    public interface IFetchOption : IOption
    {
        bool All { get; }
        bool Username { get; }
        bool Password { get; }
    }
}
