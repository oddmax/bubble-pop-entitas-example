using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class CalculateTrajectorySystem : ReactiveSystem<InputEntity>
{
    readonly Contexts contexts;
    private readonly IGroup<GameEntity> launcher;
    private readonly IGroup<GameEntity> bubblePreview;
    private const int maxStepDistance = 200;

    public CalculateTrajectorySystem(Contexts contexts) : base(contexts.input)
    {
        this.contexts = contexts;
        launcher = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Launcher));
        bubblePreview = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.BubblePreview));
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        => context.CreateCollector(InputMatcher.AllOf(InputMatcher.Input, InputMatcher.MouseDown));

    protected override bool Filter(InputEntity entity) => entity.isInput && entity.hasMouseDown;

    protected override void Execute(List<InputEntity> entities)
    {
        var inputEntity = entities.SingleEntity();
        var input = inputEntity.isInput;
        
        var launcherEntity = launcher.GetSingleEntity();
        var previewEntity = bubblePreview.GetSingleEntity();

        var launcherPosition = launcherEntity.transformPosition.value;
        var direction = inputEntity.mouseDown.value.value - launcherPosition;
        
        PredictReflectionPattern(launcherPosition, direction, launcherEntity, previewEntity);
    }
    
    private void PredictReflectionPattern(Vector2 position, Vector2 direction, GameEntity launcherEntity, GameEntity previewEntity)
    {
        bool isReflected = false;
        bool isHitBubble = false;
        Vector2 ReflectionPoint = new Vector2();
        Vector2 TargetPoint = new Vector2();
        Vector2Int HitBubblePosition = new Vector2Int();
        Vector2 HitBubbleTransformPosition = new Vector2();

        var startingPosition = position;
    
        RaycastHit2D hit = Physics2D.Raycast(position, direction, maxStepDistance);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Bubble")
            {
                TargetPoint = hit.point;
                HitBubblePosition = hit.collider.gameObject.GetComponent<BoardBubbleView>().bubblePosition;
                HitBubbleTransformPosition = hit.collider.transform.position;
                isHitBubble = true;
            }
            else if (hit.collider.tag == "Wall")
            {
                direction = Vector2.Reflect(direction, hit.normal);
                position = hit.point;
                isReflected = true;
                ReflectionPoint = position;
                
                if (position.x < startingPosition.x)
                    position.x += 0.01f;
    
                if (position.x > startingPosition.x)
                    position.x -= 0.01f;
            
                RaycastHit2D secondHit = Physics2D.Raycast(position, direction, maxStepDistance);
                if (secondHit.collider != null && secondHit.collider.tag == "Bubble")
                {
                    TargetPoint = secondHit.point;
                    HitBubblePosition = secondHit.collider.gameObject.GetComponent<BoardBubbleView>().bubblePosition;
                    HitBubbleTransformPosition = secondHit.collider.transform.position;
                    isHitBubble = true;
                }
            }
        }

        if (isHitBubble == false)
        {
            launcherEntity.ReplaceVisible(false);
            previewEntity.ReplaceVisible(false);
        }
        else
        {
            launcherEntity.ReplaceLauncherTrajectory(isReflected, ReflectionPoint, TargetPoint, HitBubbleTransformPosition);
            Vector2Int previewBubbleNewPosition = CalculateNewPreviewBubblePosition(HitBubblePosition, HitBubbleTransformPosition, TargetPoint);
            if (!previewEntity.hasPosition)
            {
                previewEntity.AddPosition(previewBubbleNewPosition);
            }
            if (previewEntity.hasPosition && previewEntity.position.value != previewBubbleNewPosition)
            {
                 previewEntity.ReplacePosition(previewBubbleNewPosition);
            }
            launcherEntity.ReplaceVisible(true);
            previewEntity.ReplaceVisible(true);
        } 
    }

    private Vector2Int CalculateNewPreviewBubblePosition(Vector2Int bubbleCoor, Vector2 hitBubblePosition,
        Vector2 hitPointPosition)
    {
        Vector2Int bubblePreviewCoor = bubbleCoor; 
        //right neighbour
        if (hitPointPosition.x > hitBubblePosition.x && hitPointPosition.y > hitBubblePosition.y)
        {
            bubblePreviewCoor.x += 2;
            if (contexts.game.GetBubbleWithPosition(bubblePreviewCoor) != null)
            {
                bubblePreviewCoor = bubbleCoor;
                bubblePreviewCoor.x += 1;
                bubblePreviewCoor.y += 1;
            }
        }
        
        //left neighbour
        if (hitPointPosition.x <= hitBubblePosition.x && hitPointPosition.y > hitBubblePosition.y)
        {
            bubblePreviewCoor.x -= 2;
            if (contexts.game.GetBubbleWithPosition(bubblePreviewCoor) != null)
            {
                bubblePreviewCoor = bubbleCoor;
                bubblePreviewCoor.x -= 1;
                bubblePreviewCoor.y += 1;
            }
        }
        
        //right bottom neighbour
        if (hitPointPosition.x > hitBubblePosition.x && hitPointPosition.y <= hitBubblePosition.y)
        {
            bubblePreviewCoor.x += 1;
            bubblePreviewCoor.y += 1;
            if (contexts.game.GetBubbleWithPosition(bubblePreviewCoor) != null)
            {
                bubblePreviewCoor = bubbleCoor;
                bubblePreviewCoor.x -= 1;
                bubblePreviewCoor.y += 1;
            }
        }
        
        //left bottom neighbour
        if (hitPointPosition.x <= hitBubblePosition.x && hitPointPosition.y <= hitBubblePosition.y)
        {
            bubblePreviewCoor.x -= 1;
            bubblePreviewCoor.y += 1;
            if (contexts.game.GetBubbleWithPosition(bubblePreviewCoor) != null)
            {
                bubblePreviewCoor = bubbleCoor;
                bubblePreviewCoor.x += 1;
                bubblePreviewCoor.y += 1;
            }
        }

        return bubblePreviewCoor;
    }
}
