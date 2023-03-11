using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiRayChecker : MonoBehaviour
{
    public Ray_Points[] points;
    public LayerMask collisionMask = new LayerMask();
    public bool vertical = false;
    public bool horizontal = false;

    public abstract Vector2 Start { get; }

    public abstract Vector2 End { get; }

    public GameObject CollisionObject
    {
        get;
        set;
    }

    public class Ray_Points
    {
        public Vector2 start;
        public Vector2 end;

        public Ray_Points(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }
    }

    public bool CheckCollision(float width, int divisions, float distance)
    {
        Ray_Points[] p = points = BuildArray(width, divisions);
        RaycastHit2D hit;

        foreach (Ray_Points point in p)
        {
            hit = Physics2D.Raycast(point.start, point.end, distance, collisionMask);
            if (CheckResult(hit))
                return true;
        }

        return false;
    }

    private bool CheckResult(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            CollisionObject = hit.collider.gameObject;
            return true;
        }
        return false;
    }

    private Ray_Points[] BuildArray(float width, int divisions)
    {
        List<Ray_Points> rayPoints = new List<Ray_Points>();
        float step = width / (divisions * 2);
        float currentStep = step;

        for (int i = 0; i < divisions; i++)
        {
            if (horizontal)
            {
                rayPoints.Add(new Ray_Points(new Vector2(Start.x - currentStep, Start.y), new Vector2(End.x - currentStep, End.y)));
                rayPoints.Add(new Ray_Points(new Vector2(Start.x + currentStep, Start.y), new Vector2(End.x + currentStep, End.y)));
            }

            if (vertical)
            {
                rayPoints.Add(new Ray_Points(new Vector2(Start.x, Start.y - currentStep), new Vector2(End.x, End.y - currentStep)));
                rayPoints.Add(new Ray_Points(new Vector2(Start.x, Start.y + currentStep), new Vector2(End.x, End.y + currentStep)));
            }

            currentStep += step;
        }

        return rayPoints.ToArray();
    }

#if DEBUG
    internal void DrawGizmos(float width, int divisions, float distance)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(Start, End);
        Ray_Points[] rayPoints = BuildArray(width, divisions);

        foreach (Ray_Points p in rayPoints)
        {
            Gizmos.DrawLine(p.start, p.end);
        }

        Gizmos.DrawLine(Start, End);

        Gizmos.DrawCube(Start, new Vector3(0.05f, 0.05f, 0.05f));
        Gizmos.DrawCube(End, new Vector3(0.05f, 0.05f, 0.05f));
        Gizmos.DrawCube(hit2D.point, new Vector3(0.05f, 0.05f, 0.05f));

    }
#endif
}