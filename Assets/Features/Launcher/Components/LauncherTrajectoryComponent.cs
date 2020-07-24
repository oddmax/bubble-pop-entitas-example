using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Event(EventTarget.Self)]
public sealed class LauncherTrajectoryComponent : IComponent
{
    public bool isReflected;
    public Vector2 ReflectionPoint;
    public Vector2 TargetPoint;
    public Vector2 TargetBubbleCoordinates;
    
}
