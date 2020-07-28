using DG.Tweening;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class LauncherTrajectoryView : MonoBehaviour, IView, ITransformPositionListener, IDestroyedListener, ILauncherTrajectoryListener, IVisibleListener
{
    public LineRenderer LineRenderer;
    private Vector2[] pointsArray;

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
    
    public void OnLauncherTrajectory(GameEntity entity, Vector2 targetBubbleCoordinates, Vector2[] shownTrajectoryPointsArray,
        Vector2[] actualTrajectoryPointsArray)
    {
        this.pointsArray = shownTrajectoryPointsArray;
        LineRenderer.positionCount = pointsArray.Length;
        for (int i = 0; i < pointsArray.Length; i++)
        {
            LineRenderer.SetPosition(i, pointsArray[i]);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(pointsArray == null)
            return;
        
        for (int i = 1; i < pointsArray.Length; i++)
        {
            Gizmos.DrawLine(pointsArray[i-1], pointsArray[i]);
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
