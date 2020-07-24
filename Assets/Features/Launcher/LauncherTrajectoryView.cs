using DG.Tweening;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class LauncherTrajectoryView : MonoBehaviour, IView, ITransformPositionListener, IDestroyedListener, ILauncherTrajectoryListener, IVisibleListener
{
    public LineRenderer LineRenderer;
    public bool isReflected;
    public Vector2 ReflectionPoint;
    public Vector2 TargetPoint;
    public void Link(IEntity entity)
    {
        gameObject.Link(entity);
        var e = (GameEntity)entity;
        e.AddTransformPositionListener(this);
        e.AddDestroyedListener(this);
        e.AddLauncherTrajectoryListener(this);
        e.AddVisibleListener(this);
        Vector3 newPosition = e.transformPosition.value;
        newPosition.z = 1;
        transform.position = newPosition;
        LineRenderer.enabled = false;
    }

    public void SetSpritesInfo(BubbleSprite[] bubbleSprites)
    {
        
    }

    public void OnTransformPosition(GameEntity entity, Vector2 value)
    {
        transform.DOKill();
        Vector3 newPosition = entity.transformPosition.value;
        newPosition.z = 1;
        transform.position = newPosition;
    }
    
    public void OnLauncherTrajectory(GameEntity entity, bool isReflected, Vector2 reflectionPoint, Vector2 targetPoint, Vector2 targetBubbleCoordinates)
    {
        this.isReflected = isReflected;
        ReflectionPoint = reflectionPoint;
        TargetPoint = targetPoint;

        if (isReflected)
        {
            LineRenderer.positionCount = 3;
            LineRenderer.SetPosition(0, transform.position);
            LineRenderer.SetPosition(1, ReflectionPoint);
            LineRenderer.SetPosition(2, TargetPoint);
        }
        else
        {
            LineRenderer.positionCount = 2;
            LineRenderer.SetPosition(0, transform.position);
            LineRenderer.SetPosition(1, TargetPoint);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (isReflected)
        {
            Gizmos.DrawLine(transform.position, ReflectionPoint);
            Gizmos.DrawLine(ReflectionPoint, TargetPoint);
        }
        else
        {
            Gizmos.DrawLine(transform.position, TargetPoint);
 
        }
    }

    public void OnDestroyed(GameEntity entity)
    {
        
    }


    public void OnVisible(GameEntity entity, bool isVisible)
    {
        LineRenderer.enabled = isVisible;
    }
}
