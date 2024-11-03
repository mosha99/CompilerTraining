public class Terminal : IAggregateParser
{
    public List<Aggregate> Aggregate { set; get; } = [];

    public bool ParsTerminal(string input)
    {
        return TryPars(ref input) && !input.Any();
    }

    public bool TryPars(ref string input)
    {
        return TryPars(0, Aggregate, ref input);
    }
    private bool TryPars(int index, List<Aggregate> aggregates, ref string input)
    {
        if (!aggregates.Any()) throw new Exception("Can Not Pars");

        var hasEpsilon = aggregates.Any(x => x.First().Contains(null));

        if (string.IsNullOrEmpty(input)) 
            return hasEpsilon;

        while (true)
        {
            var str = input;
            aggregates = aggregates.Where(x => x.First(index, []).Contains(str.ToList()[index])).ToList()!;
            if (!aggregates!.Any())
            {
                return hasEpsilon;
            };
            if (aggregates.Count() == 1) break;
        }

        Aggregate target = aggregates.First();

        return target.TryPars(ref input);
    }
    public List<char?> First()
    {
        return Aggregate.SelectMany(x => x.First()).ToList();
    }
}