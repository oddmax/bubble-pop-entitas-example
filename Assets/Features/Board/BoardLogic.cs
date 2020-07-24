using UnityEngine;

public static class BoardLogic
{
    public static Vector2Int[] BubbleNeighboursOffsets = new[]
    {
        new Vector2Int(2, 0),
        new Vector2Int(-1, -1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, 1),
        new Vector2Int(1, 1),
        new Vector2Int(-2, 0)
    };
    
    public static int GetMergeNumber(int bubbleNumber, int bubblesAmount)
    {
        if (bubblesAmount < 2)
            return bubbleNumber;

        if (bubblesAmount > 12)
            return 1 << 12;
        
        return bubbleNumber * (1 << (bubblesAmount-1));
    }

    public static bool IsRowShifted(int row, bool isFirstRowShifted)
    {
        if (isFirstRowShifted)
            return row % 2 == 0;
        
        return row % 2 == 1;
    }

    public static void GetColumnStartEnd(int row, bool isFirstRowShifted, int boardWidth, out int columnStart, out int columnEnd)
    {
        var isRowEven = row % 2 == 0;
        if (isRowEven)
            columnStart = isFirstRowShifted ? 1 : 0;
        else 
            columnStart = isFirstRowShifted ? 0 : 1;
        
        columnEnd = boardWidth * 2 + columnStart - 1;
    }

}
