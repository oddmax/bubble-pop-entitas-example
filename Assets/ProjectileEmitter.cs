using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEmitter : MonoBehaviour
{
    [SerializeField] private int maxReflectionCount = 1;

    public float maxStepDistance = 200;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        DrawPredictedReflectionPattern(this.transform.position, this.transform.up, maxReflectionCount);
    }

    private void DrawPredictedReflectionPattern(Vector2 position, Vector2 direction, int reflectionRemaining)
    {
        if(reflectionRemaining == 0)
            return;

        var startingPosition = position;
        
        RaycastHit2D hit = Physics2D.Raycast(position, direction, maxStepDistance);
        if (hit.collider != null)
        {
            direction = Vector2.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            position += direction * maxStepDistance;
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startingPosition, position);

        if (position.x < startingPosition.x)
            position.x += 0.01f;
        
        if (position.x > startingPosition.x)
            position.x -= 0.01f;
        
        DrawPredictedReflectionPattern(position, direction, reflectionRemaining - 1);
    }
}
