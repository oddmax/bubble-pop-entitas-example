using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class AddBubbleViewSystem : ReactiveSystem<GameEntity>
{
    readonly Transform boardParent;
    private readonly BubbleSprite[] bubbleSprites;

    public AddBubbleViewSystem(Contexts contexts) : base(contexts.game)
    {
        boardParent = GameObject.FindWithTag("Board").transform;
        bubbleSprites = contexts.config.gameConfig.value.BubbleSprites;
        
        contexts.game.isBubblePreview = true;
        var bubblePreviewEntity = contexts.game.bubblePreviewEntity;
        bubblePreviewEntity.AddAsset("BubblePreview");
        bubblePreviewEntity.AddTransformPosition(GameObject.FindWithTag("Preview").transform.position);
        bubblePreviewEntity.AddVisible(false);
        
        contexts.game.isLauncher = true;

        contexts.game.CreateLauncherBubble();
            
        var trajectoryEntity = contexts.game.launcherEntity;
        trajectoryEntity.AddAsset("BubbleLauncherTrajectory");
        trajectoryEntity.AddTransformPosition(GameObject.FindWithTag("Launcher").transform.position);
        trajectoryEntity.AddVisible(false);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.Asset);

    protected override bool Filter(GameEntity entity) => entity.hasAsset && !entity.isParticle && !entity.isTextSpawn && !entity.hasView && !entity.isDestroyed;

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
            e.AddView(instantiateView(e));
    }

    IView instantiateView(GameEntity entity)
    {
        var prefab = Resources.Load<GameObject>(entity.asset.value);
        var view = Object.Instantiate(prefab, boardParent).GetComponent<IView>();
        view.SetSpritesInfo(bubbleSprites);
        view.Link(entity);
        return view;
    }
}
