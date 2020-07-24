//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity bubblePreviewEntity { get { return GetGroup(GameMatcher.BubblePreview).GetSingleEntity(); } }

    public bool isBubblePreview {
        get { return bubblePreviewEntity != null; }
        set {
            var entity = bubblePreviewEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isBubblePreview = true;
                } else {
                    entity.Destroy();
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly BubblePreviewComponent bubblePreviewComponent = new BubblePreviewComponent();

    public bool isBubblePreview {
        get { return HasComponent(GameComponentsLookup.BubblePreview); }
        set {
            if (value != isBubblePreview) {
                var index = GameComponentsLookup.BubblePreview;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : bubblePreviewComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherBubblePreview;

    public static Entitas.IMatcher<GameEntity> BubblePreview {
        get {
            if (_matcherBubblePreview == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BubblePreview);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBubblePreview = matcher;
            }

            return _matcherBubblePreview;
        }
    }
}