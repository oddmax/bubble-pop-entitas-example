
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Unique, Timer]
public sealed class MergeProgressComponent : IComponent
{
    public Vector2Int mergeBubblePosition;
    public int mergeNumber;
    public float time;
}
