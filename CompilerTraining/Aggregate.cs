public class Aggregate : IAggregateParser
{
    public required List<IAggregateParser> Parsers { set; get; }
    public List<char?> First()
    {
        return First(0, []);
    }

    public List<char?> First(int index, List<char?> chars)
    {
        if (Parsers.Count() <= index) return [];

        var firsts = Parsers[index].First();

        List<List<char?>> results = [firsts, chars];

        chars = results.SelectMany(x => x).Distinct().ToList();

        return chars;
    }

    public bool TryPars(ref string input)
    {
        foreach (var p in Parsers)
        {
            if (p.TryPars(ref input)) continue;
            if (p is Epsilon) continue;
            return false;
        }

        return true;
    }

    public static Aggregate Epsilon => new Aggregate()
    {
        Parsers = [new Epsilon()]
    };
}