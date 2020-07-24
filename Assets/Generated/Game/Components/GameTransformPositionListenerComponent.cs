//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TransformPositionListenerComponent transformPositionListener { get { return (TransformPositionListenerComponent)GetComponent(GameComponentsLookup.TransformPositionListener); } }
    public bool hasTransformPositionListener { get { return HasComponent(GameComponentsLookup.TransformPositionListener); } }

    public void AddTransformPositionListener(System.Collections.Generic.List<ITransformPositionListener> newValue) {
        var index = GameComponentsLookup.TransformPositionListener;
        var component = (TransformPositionListenerComponent)CreateComponent(index, typeof(TransformPositionListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTransformPositionListener(System.Collections.Generic.List<ITransformPositionListener> newValue) {
        var index = GameComponentsLookup.TransformPositionListener;
        var component = (TransformPositionListenerComponent)CreateComponent(index, typeof(TransformPositionListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTransformPositionListener() {
        RemoveComponent(GameComponentsLookup.TransformPositionListener);
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
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherTransformPositionListener;

    public static Entitas.IMatcher<GameEntity> TransformPositionListener {
        get {
            if (_matcherTransformPositionListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TransformPositionListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTransformPositionListener = matcher;
            }

            return _matcherTransformPositionListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddTransformPositionListener(ITransformPositionListener value) {
        var listeners = hasTransformPositionListener
            ? transformPositionListener.value
            : new System.Collections.Generic.List<ITransformPositionListener>();
        listeners.Add(value);
        ReplaceTransformPositionListener(listeners);
    }

    public void RemoveTransformPositionListener(ITransformPositionListener value, bool removeComponentWhenEmpty = true) {
        var listeners = transformPositionListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveTransformPositionListener();
        } else {
            ReplaceTransformPositionListener(listeners);
        }
    }
}