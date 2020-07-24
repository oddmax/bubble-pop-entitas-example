using System.Collections.Generic;
using Entitas;
using UnityEngine;

public partial class Contexts
{
    public const string Bubble = "Bubble";

    [Entitas.CodeGeneration.Attributes.PostConstructor]
    public void InitializePieceEntityIndices()
    {
        game.AddEntityIndex(new PrimaryEntityIndex<GameEntity, Vector2Int>(
            Bubble,
            game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Bubble, GameMatcher.Position)
                .NoneOf(GameMatcher.Destroyed)
            ),
            (e, c) => (c as PositionComponent)?.value ?? e.position.value));
    }
}

public static class ContextsExtensions
{
    public static GameEntity GetBubbleWithPosition(this GameContext context, Vector2Int value)
    {
        return ((PrimaryEntityIndex<GameEntity, Vector2Int>)context.GetEntityIndex(Contexts.Bubble)).GetEntity(value);
    }
    
    public static List<GameEntity> GetBubbleNeighbours(this GameContext context, Vector2Int value)
    {
        var neighbours = new List<GameEntity>();
        foreach (var bubbleNeighboursOffset in BoardLogic.BubbleNeighboursOffsets)
        {
            context.AddNeighboursIfExists(neighbours, value.x + bubbleNeighboursOffset.x, value.y + bubbleNeighboursOffset.y);
        }
        
        return neighbours;
    }

    public static void AddNeighboursIfExists(this GameContext context, List<GameEntity> neighbours, int x, int y)
    {
        var bubbleEntity = context.GetBubbleWithPosition(new Vector2Int(x, y));
        if(bubbleEntity != null) 
            neighbours.Add(bubbleEntity);
    }
}
