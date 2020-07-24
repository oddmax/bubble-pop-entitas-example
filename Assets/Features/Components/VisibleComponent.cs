using Entitas;
using Entitas.CodeGeneration.Attributes;

[Event(EventTarget.Self)]
public class VisibleComponent : IComponent
{
    public bool isVisible;
}
