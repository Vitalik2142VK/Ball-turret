using System;

public class ActorPlanner : IActorPlanner
{
    private const int MinIndexInLine = 0;
    private const int MaxIndexInLine = 2;
    private const int MinIndexInColumn = 0;
    private const int MaxIndexInColumn = 2;

    public ActorPlanner(string nameActor, int lineNumber, int columnNumber)
    {
        if (lineNumber < MinIndexInLine || lineNumber > MaxIndexInLine)
            throw new ArgumentOutOfRangeException(nameof(lineNumber));

        if (columnNumber < MinIndexInColumn || columnNumber > MaxIndexInColumn)
            throw new ArgumentOutOfRangeException(nameof(columnNumber));

        NameActor = nameActor ?? throw new ArgumentNullException(nameof(nameActor));
        LineNumber = lineNumber;
        ColumnNumber = columnNumber;
    }

    public string NameActor { get; }
    public int LineNumber { get; }
    public int ColumnNumber { get; }
}
