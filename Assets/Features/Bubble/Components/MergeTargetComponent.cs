using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Event(EventTarget.Self)]
public sealed class MergeTargetComponent : IComponent
{
    public Vector2Int value;
}
