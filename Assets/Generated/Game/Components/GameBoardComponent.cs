//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity boardEntity { get { return GetGroup(GameMatcher.Board).GetSingleEntity(); } }
    public BoardComponent board { get { return boardEntity.board; } }
    public bool hasBoard { get { return boardEntity != null; } }

    public GameEntity SetBoard(UnityEngine.Vector2Int newValue, bool newIsFirstRowShifted) {
        if (hasBoard) {
            throw new Entitas.EntitasException("Could not set Board!\n" + this + " already has an entity with BoardComponent!",
                "You should check if the context already has a boardEntity before setting it or use context.ReplaceBoard().");
        }
        var entity = CreateEntity();
        entity.AddBoard(newValue, newIsFirstRowShifted);
        return entity;
    }

    public void ReplaceBoard(UnityEngine.Vector2Int newValue, bool newIsFirstRowShifted) {
        var entity = boardEntity;
        if (entity == null) {
            entity = SetBoard(newValue, newIsFirstRowShifted);
        } else {
            entity.ReplaceBoard(newValue, newIsFirstRowShifted);
        }
    }

    public void RemoveBoard() {
        boardEntity.Destroy();
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

    public BoardComponent board { get { return (BoardComponent)GetComponent(GameComponentsLookup.Board); } }
    public bool hasBoard { get { return HasComponent(GameComponentsLookup.Board); } }

    public void AddBoard(UnityEngine.Vector2Int newValue, bool newIsFirstRowShifted) {
        var index = GameComponentsLookup.Board;
        var component = (BoardComponent)CreateComponent(index, typeof(BoardComponent));
        component.value = newValue;
        component.isFirstRowShifted = newIsFirstRowShifted;
        AddComponent(index, component);
    }

    public void ReplaceBoard(UnityEngine.Vector2Int newValue, bool newIsFirstRowShifted) {
        var index = GameComponentsLookup.Board;
        var component = (BoardComponent)CreateComponent(index, typeof(BoardComponent));
        component.value = newValue;
        component.isFirstRowShifted = newIsFirstRowShifted;
        ReplaceComponent(index, component);
    }

    public void RemoveBoard() {
        RemoveComponent(GameComponentsLookup.Board);
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

    static Entitas.IMatcher<GameEntity> _matcherBoard;

    public static Entitas.IMatcher<GameEntity> Board {
        get {
            if (_matcherBoard == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Board);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBoard = matcher;
            }

            return _matcherBoard;
        }
    }
}
