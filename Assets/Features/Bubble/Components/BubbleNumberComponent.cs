using Entitas;
using Entitas.CodeGeneration.Attributes;

[Event(EventTarget.Self)]
public sealed class BubbleNumberComponent : IComponent
{
    public int Number;
}
