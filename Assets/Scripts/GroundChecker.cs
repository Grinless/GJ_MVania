using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for monitoring ground beneath the player. 
/// </summary>
[System.Serializable]
public class GroundChecker : MonoBehaviour
{
    private const float JUMP_DETECTION_DIST = .75f;
    private const float JUMP_DETECTION_LENGTH = 0.25f;
    private const float DIVISIONS = 5;
    private const float RAYCAST_WIDTH = 1.2f;

    public bool grounded;
    public GameObject collRes;
    public LayerMask collisionMask = new LayerMask();

    public bool Grounded => grounded = CheckGround();
    public GameObject collisionResult
    {
        get => collRes;
        private set => collRes = value;
    }

    private Vector2 Start
    {
        get
        {
            return new Vector2(transform.position.x,
                transform.position.y - JUMP_DETECTION_DIST);
        }
    }

    private Vector2 End
    {
        get
        {
            return new Vector2(transform.position.x,
                transform.position.y - (JUMP_DETECTION_DIST + JUMP_DETECTION_LENGTH));
        }
    }

    private bool CheckGround()
    {
        RayPoints[] points = BuildRayVectors();
        RaycastHit2D hit = Physics2D.Raycast(Start, End, JUMP_DETECTION_LENGTH, collisionMask);
        bool found = CheckResult(hit);

        //Check easiest cast first. 
        if(found)
            return found;

        foreach (RayPoints point in points)
        {
            hit = Physics2D.Raycast(point.start, point.end, JUMP_DETECTION_LENGTH, collisionMask);
            if (CheckResult(hit))
                return true;
        }

        return false;
    }

    private class RayPoints
    {
        public Vector2 start;
        public Vector2 end; 

        public RayPoints(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }
    }

    private RayPoints[] BuildRayVectors()
    {
        List<RayPoints> rayPoints = new List<RayPoints>();
        float step = RAYCAST_WIDTH / (DIVISIONS * 2);
        float currentStep = step;

        for (int i = 0; i < DIVISIONS; i++)
        {
            rayPoints.Add(new RayPoints(new Vector2(Start.x - currentStep, Start.y), new Vector2(End.x - currentStep, End.y)));
            rayPoints.Add(new RayPoints(new Vector2(Start.x + currentStep, Start.y), new Vector2(End.x + currentStep, End.y)));

            currentStep += step;
        }

        return rayPoints.ToArray();
    }

    private bool CheckResult(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            collisionResult = hit.collider.gameObject;
            return true;
        }
        return false;
    }

#if DEBUG
    private void OnDrawGizmosSelected()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(Start, End);
        RayPoints[] rayPoints = BuildRayVectors();

        foreach (RayPoints p in rayPoints)
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
