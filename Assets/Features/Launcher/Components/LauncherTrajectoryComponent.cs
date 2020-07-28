using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Event(EventTarget.Self)]
public sealed class LauncherTrajectoryComponent : IComponent
{
    public Vector2 TargetBubbleCoordinates;
    public Vector2[] shownTrajectoryPointsArray;
    public Vector2[] actualTrajectoryPointsArray;
}
