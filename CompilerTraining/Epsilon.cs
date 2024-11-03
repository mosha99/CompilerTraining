public class Epsilon() : IAggregateParser
{
    public bool TryPars(ref string input)
        => true;

    public List<char?> First()
    {
        return [null];
    }
}