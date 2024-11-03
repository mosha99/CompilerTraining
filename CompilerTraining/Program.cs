G1();
Console.WriteLine(new string('-', 50));
G2();
Console.WriteLine(new string('-', 50));
G3();


void G1()
{
    Terminal S = new Terminal();

    Aggregate S1 = new Aggregate()
    {
        Parsers = [new NonTerminal('+'), S, S]
    };

    Aggregate S2 = new Aggregate()
    {
        Parsers = [new NonTerminal('-'), S, S]
    };

    Aggregate S3 = new Aggregate()
    {
        Parsers = [new NonTerminal('a')]
    };

    S.Aggregate = [S1, S2, S3];

    string[] testItems = ["+aa", "-aa", "++aa-a", "++aa-aa", "+a+a-a"];
    Run(testItems, S, "S => +SS | -SS | a");

}
void G2()
{
    Terminal S = new Terminal();
    Terminal SP = new Terminal();

    Aggregate S0 = new Aggregate()
    {
        Parsers = [SP]
    };

    Aggregate SP0 = new Aggregate()
    {
        Parsers = [new NonTerminal('('), S, new NonTerminal(')'), S, SP]
    };
    Aggregate SP1 = new Aggregate()
    {
        Parsers = [new Epsilon()]
    };

    S.Aggregate = [S0];
    SP.Aggregate = [SP0, SP1];

    string[] testItems = ["", "()", "(", ")", "((())", "(()))", "((((((()())(()()))))()))", "(()", "())"];
    Run(testItems, S, "S => S(S)S | EPSILON");
}
void G3()
{
    Terminal S = new Terminal();
    Terminal A = new Terminal();
    Terminal B = new Terminal();


    Aggregate S0 = new Aggregate()
    {
        Parsers = [new NonTerminal('a'), A, new NonTerminal('b'), new NonTerminal('b')]
    };

    Aggregate S1 = new Aggregate()
    {
        Parsers = [new NonTerminal('b'), B, new NonTerminal('a'), new NonTerminal('a')]
    };

    S.Aggregate = [S0, S1];

    Aggregate A0 = new Aggregate()
    {
        Parsers = [new NonTerminal('a'), A,B]
    };   
    
    Aggregate A1 = Aggregate.Epsilon;

    A.Aggregate = [A0, A1];

    Aggregate B0 = new Aggregate()
    {
        Parsers = [new NonTerminal('b'), B,B]
    };   
    
    Aggregate B1 = new Aggregate()
    {
        Parsers = [new NonTerminal('d')]
    };
    B.Aggregate = [B0, B1];



    string[] testItems = ["abb","aabddbb", "bbddaa", "bbbdddaa", "baaa", "aabb", "abbaa", "aaabb", "bbb"];
    Run(testItems, S, "S => S(S)S | EPSILON");
}



void Run(string[] tests, Terminal terminal, string title)
{
    Console.WriteLine(title);
    Console.WriteLine();

    foreach (var i in tests)
    {
        var match = terminal.ParsTerminal(i);
        Console.WriteLine($"'{i}' - {match}");
    }
}