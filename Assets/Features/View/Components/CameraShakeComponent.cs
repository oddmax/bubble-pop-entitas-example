
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Event(EventTarget.Any)]
public sealed class CameraShakeComponent : IComponent
{
    public int amountOfShakes;
}
