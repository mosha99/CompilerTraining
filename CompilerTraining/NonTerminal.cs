public class NonTerminal(char ch) : IAggregateParser
{
    public bool TryPars(ref string input)
    {
        if (input.Any() == false || input[0] != ch) return false;

        input = input.Remove(0, 1);

        return true;
    }

    public List<char?> First()
    {
        return [ch];
    }
}