using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Config, Unique, ComponentName("GameConfig")]
public interface IGameConfig
{
    Vector2Int boardSize { get; }

    int MinAmountOfRows { get; }
    int MediumAmountOfRows { get; }
    int MaxAmountOfRows { get; }
    BubbleSprite[] BubbleSprites { get; }
    
    float BubbleSize { get; }
    
    float MovementSpeed { get; }
    
    float MergeTimeInSeconds { get; }
}
