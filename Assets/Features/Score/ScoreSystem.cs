using System.Collections.Generic;
using Entitas;

public sealed class ScoreSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    readonly Contexts contexts;

    public ScoreSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
    }

    public void Initialize()
    {
        contexts.gameState.SetScore(0);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.Destroyed);

    protected override bool Filter(GameEntity entity) => entity.isDestroyed && entity.isBubble;

    protected override void Execute(List<GameEntity> entities)
    {
        contexts.gameState.ReplaceScore(contexts.gameState.score.value + entities.Count);
    }
}
