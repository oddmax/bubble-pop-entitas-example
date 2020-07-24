
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Timer]
public class TimerComponent : IComponent
{
    public float remaining;
}

[Timer]
public class TimerRunningComponent : IComponent
{
}

[Timer, FlagPrefix("will")]
public class DestroyWhenTimerExpiresComponent : IComponent
{
}

public class TimerSystem : IExecuteSystem
{
    private readonly TimerContext timer;
    private readonly IGroup<TimerEntity> runningTimers;

    public TimerSystem(Contexts contexts)
    {
        timer = contexts.timer;
        runningTimers = timer.GetGroup(
            TimerMatcher.AllOf(
                TimerMatcher.Timer,
                TimerMatcher.TimerRunning
            )
        );
    }

    public void Execute()
    {
        var delta = Time.deltaTime;
        foreach (var e in runningTimers.GetEntities())
        {
            e.timer.remaining -= delta;

            if (e.timer.remaining <= 0.0f)
            {
                e.timer.remaining = 0.0f;
                e.isTimerRunning = false;

                /*if (e.willDestroyWhenTimerExpires)
                    // If we want to destroy this timer when it expires...
                {
                    e.isDestroyed = true;
                }*/
            }
        }
    }
}
