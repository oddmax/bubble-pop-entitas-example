using Features.Board.Systems;
using Features.Launcher;
using Features.View;

public sealed class GameSystems : Feature
{
    public GameSystems(Contexts contexts)
    {
        // Input
        Add(new InputSystem(contexts));
        Add(new CalculateTrajectorySystem(contexts));

        // Update
        Add(new BoardSystem(contexts));
        Add(new ScoreSystem(contexts));
        Add(new MergeSystem(contexts));
        Add(new LaunchBubbleSystem(contexts));
        Add(new CompleteMergeSystem(contexts));
        Add(new DestroyHangingClustersSystem(contexts));
        Add(new TimerSystem(contexts));
        Add(new MoveRowsSystem(contexts));

        // View
        Add(new AddBubbleViewSystem(contexts));
        Add(new SpawnParticleViewSystem(contexts));
        Add(new SpawnTextViewSystem(contexts));

        // Events (Generated)
        Add(new InputEventSystems(contexts));
        Add(new GameEventSystems(contexts));
        Add(new GameStateEventSystems(contexts));
    }
}
