using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Match One/Game Config")]
public class ScriptableGameConfig : ScriptableObject, IGameConfig
{
    [SerializeField] 
    Vector2Int _boardSize; 
    
    public Vector2Int boardSize => _boardSize;
    
    [SerializeField] 
    int minAmountOfRows;

    public int MinAmountOfRows => minAmountOfRows;
    
    [SerializeField] 
    int mediumAmountOfRows;

    public int MediumAmountOfRows => mediumAmountOfRows;
    
    [SerializeField] 
    int maxAmountOfRows;

    public int MaxAmountOfRows => maxAmountOfRows;
    
    [SerializeField] 
    private BubbleSprite[] bubbleSprites;

    public BubbleSprite[] BubbleSprites => bubbleSprites;
    
    [SerializeField] 
    private float bubbleSize;

    public float BubbleSize => bubbleSize;
    
    [SerializeField] 
    private float movementSpeed;

    public float MovementSpeed => movementSpeed;

    [SerializeField]
    private float mergeTimeInSeconds;
    
    public float MergeTimeInSeconds => mergeTimeInSeconds;
    
    [SerializeField]
    private int maxAmountOfReflections;
    
    public int MaxAmountOfReflections  => maxAmountOfReflections;
}

[Serializable]
public struct BubbleSprite
{
    public int number;
    public Sprite sprite;
}
