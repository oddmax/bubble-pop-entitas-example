//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CurrentTrajectoryIndexComponent currentTrajectoryIndex { get { return (CurrentTrajectoryIndexComponent)GetComponent(GameComponentsLookup.CurrentTrajectoryIndex); } }
    public bool hasCurrentTrajectoryIndex { get { return HasComponent(GameComponentsLookup.CurrentTrajectoryIndex); } }

    public void AddCurrentTrajectoryIndex(int newIndex, float newProgress) {
        var index = GameComponentsLookup.CurrentTrajectoryIndex;
        var component = (CurrentTrajectoryIndexComponent)CreateComponent(index, typeof(CurrentTrajectoryIndexComponent));
        component.index = newIndex;
        component.progress = newProgress;
        AddComponent(index, component);
    }

    public void ReplaceCurrentTrajectoryIndex(int newIndex, float newProgress) {
        var index = GameComponentsLookup.CurrentTrajectoryIndex;
        var component = (CurrentTrajectoryIndexComponent)CreateComponent(index, typeof(CurrentTrajectoryIndexComponent));
        component.index = newIndex;
        component.progress = newProgress;
        ReplaceComponent(index, component);
    }

    public void RemoveCurrentTrajectoryIndex() {
        RemoveComponent(GameComponentsLookup.CurrentTrajectoryIndex);
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

    static Entitas.IMatcher<GameEntity> _matcherCurrentTrajectoryIndex;

    public static Entitas.IMatcher<GameEntity> CurrentTrajectoryIndex {
        get {
            if (_matcherCurrentTrajectoryIndex == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CurrentTrajectoryIndex);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCurrentTrajectoryIndex = matcher;
            }

            return _matcherCurrentTrajectoryIndex;
        }
    }
}
