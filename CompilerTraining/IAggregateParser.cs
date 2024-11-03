public interface IAggregateParser
{
    public bool TryPars(ref string ch);
    public List<char?> First();
}