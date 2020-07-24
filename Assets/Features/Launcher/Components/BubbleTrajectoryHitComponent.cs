using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Event(EventTarget.Self)]
public sealed class BubbleTrajectoryHitComponent
{
    public Vector2Int hitBubbleCoordinates;
}
