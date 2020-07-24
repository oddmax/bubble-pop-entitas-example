using System.Collections.Generic;
using Entitas;

public sealed class BoardSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    readonly Contexts contexts;
    readonly IGroup<GameEntity> bubbles;

    public BoardSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
        bubbles = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Position));
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

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.Board);

    protected override bool Filter(GameEntity entity) => entity.hasBoard;

    protected override void Execute(List<GameEntity> entities)
    {
    }
}
