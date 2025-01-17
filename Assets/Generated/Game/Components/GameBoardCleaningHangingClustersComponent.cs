//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity boardCleaningHangingClustersEntity { get { return GetGroup(GameMatcher.BoardCleaningHangingClusters).GetSingleEntity(); } }

    public bool isBoardCleaningHangingClusters {
        get { return boardCleaningHangingClustersEntity != null; }
        set {
            var entity = boardCleaningHangingClustersEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isBoardCleaningHangingClusters = true;
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

    static readonly BoardCleaningHangingClustersComponent boardCleaningHangingClustersComponent = new BoardCleaningHangingClustersComponent();

    public bool isBoardCleaningHangingClusters {
        get { return HasComponent(GameComponentsLookup.BoardCleaningHangingClusters); }
        set {
            if (value != isBoardCleaningHangingClusters) {
                var index = GameComponentsLookup.BoardCleaningHangingClusters;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : boardCleaningHangingClustersComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherBoardCleaningHangingClusters;

    public static Entitas.IMatcher<GameEntity> BoardCleaningHangingClusters {
        get {
            if (_matcherBoardCleaningHangingClusters == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BoardCleaningHangingClusters);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBoardCleaningHangingClusters = matcher;
            }

            return _matcherBoardCleaningHangingClusters;
        }
    }
}
