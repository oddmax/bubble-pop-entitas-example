using Entitas;

public sealed class InitializeBoardSystem : IInitializeSystem
{
    readonly Contexts contexts;

    public InitializeBoardSystem(Contexts contexts)
    {
        this.contexts = contexts;
    }

    public void Initialize()
    {
        var entity = contexts.game.CreateEntity();
        var boardSize = contexts.config.gameConfig.value.boardSize;
        entity.AddBoard(boardSize, false);
        contexts.game.board.isFirstRowShifted = false;

        for (int y = 0; y < boardSize.y; y++)
        {
            BoardLogic.GetColumnStartEnd(y, contexts.game.board.isFirstRowShifted , boardSize.x, out var columnStart, out var columnEnd);
            for (int x = columnStart; x < columnEnd; x+=2)
            {
                contexts.game.CreateRandomBoardBubble(x, y);
            }
        }
    }
}
