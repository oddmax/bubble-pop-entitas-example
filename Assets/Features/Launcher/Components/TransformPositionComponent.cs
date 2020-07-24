using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Event(EventTarget.Self)]
public class TransformPositionComponent : IComponent
{
    public Vector2 value;
}
