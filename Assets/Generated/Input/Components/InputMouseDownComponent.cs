//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public MouseDownComponent mouseDown { get { return (MouseDownComponent)GetComponent(InputComponentsLookup.MouseDown); } }
    public bool hasMouseDown { get { return HasComponent(InputComponentsLookup.MouseDown); } }

    public void AddMouseDown(Features.Input.MouseDownComponent newValue) {
        var index = InputComponentsLookup.MouseDown;
        var component = (MouseDownComponent)CreateComponent(index, typeof(MouseDownComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMouseDown(Features.Input.MouseDownComponent newValue) {
        var index = InputComponentsLookup.MouseDown;
        var component = (MouseDownComponent)CreateComponent(index, typeof(MouseDownComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMouseDown() {
        RemoveComponent(InputComponentsLookup.MouseDown);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherMouseDown;

    public static Entitas.IMatcher<InputEntity> MouseDown {
        get {
            if (_matcherMouseDown == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.MouseDown);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherMouseDown = matcher;
            }

            return _matcherMouseDown;
        }
    }
}
