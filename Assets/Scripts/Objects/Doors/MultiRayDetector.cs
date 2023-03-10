using System.Collections.Generic;
using UnityEngine;

public enum GenerationDirection
{
    VERT_UP,
    VERT_DOWN,
    HORI_RIGHT,
    HORI_LEFT
}
public class MultiRayDetector : MonoBehaviour
{
    [SerializeField] private float JUMP_DETECTION_DIST = .5f;
    [SerializeField] private float JUMP_DETECTION_LENGTH = 0.25f;

    public int numberOfRays;
    public float spacingBetween;
    public GenerationDirection generationDirection = GenerationDirection.VERT_UP;
    public GameObject collisionResult { get; private set; }

    public Vector4[] rayVectors;

    private Vector2 StartPos
    {
        get
        {
            Vector2 pos = Vector2.zero;
            Bounds bounds = gameObject.GetComponent<BoxCollider2D>().bounds;

            switch (generationDirection)
            {
                case GenerationDirection.VERT_UP:
                    pos = new Vector2(bounds.min.x - JUMP_DETECTION_DIST, bounds.min.y);
                    break;
                case GenerationDirection.VERT_DOWN:
                    pos = new Vector2(transform.position.x - JUMP_DETECTION_DIST, transform.position.y);
                    break;
                case GenerationDirection.HORI_RIGHT:
                    pos = new Vector2(transform.position.x, transform.position.y - JUMP_DETECTION_DIST);
                    break;
                case GenerationDirection.HORI_LEFT:
                    pos = new Vector2(transform.position.x, transform.position.y - JUMP_DETECTION_DIST);
                    break;
            }

            return pos;
        }
    }

    private Vector2 EndPos
    {
        get
        {
            Vector2 pos = Vector2.zero;
            Bounds bounds = gameObject.GetComponent<BoxCollider2D>().bounds;
            float additive = (JUMP_DETECTION_DIST + JUMP_DETECTION_LENGTH);

            switch (generationDirection)
            {
                case GenerationDirection.VERT_UP:
                    pos = new Vector2(bounds.min.x - additive, bounds.min.y);
                    break;
                case GenerationDirection.VERT_DOWN:
                    pos = new Vector2(transform.position.x - additive, transform.position.y);
                    break;
                case GenerationDirection.HORI_RIGHT:
                    pos = new Vector2(transform.position.x, transform.position.y - additive);
                    break;
                case GenerationDirection.HORI_LEFT:
                    pos = new Vector2(transform.position.x, transform.position.y - additive);
                    break;
            }

            return pos;
        }
    }

    private void Awake()
    {
        rayVectors = GenerateRays();
    }

    private Vector4[] GenerateRays()
    {
        List<Vector4> rays = new List<Vector4>();

        for (int i = 0; i < numberOfRays; i++)
        {
            switch (generationDirection)
            {
                case GenerationDirection.VERT_UP:
                    rays.Add(new Vector4(StartPos.x, StartPos.y + (spacingBetween * i), EndPos.x, EndPos.y + (spacingBetween * i)));
                    break;
                case GenerationDirection.VERT_DOWN:
                    rays.Add(new Vector4(StartPos.x, StartPos.y - (spacingBetween * i), EndPos.x, EndPos.y - (spacingBetween * i)));
                    break;
                case GenerationDirection.HORI_RIGHT:
                    rays.Add(new Vector4(StartPos.x + (spacingBetween * i), StartPos.y, EndPos.x + (spacingBetween * i), EndPos.y));
                    break;
                case GenerationDirection.HORI_LEFT:
                    rays.Add(new Vector4(StartPos.x - (spacingBetween * i), StartPos.y, EndPos.x - (spacingBetween * i), EndPos.y));
                    break;
                default:
                    break;
            }
        }

        return rays.ToArray();
    }

    public GameObject[] CheckCollision(out bool collided)
    {
        List<GameObject> collidedObjects = new List<GameObject>();
        RaycastHit2D hit;

        foreach (Vector4 item in rayVectors)
        {
            hit = GetHit(item, JUMP_DETECTION_LENGTH);
            if (hit.collider != null)
            {
                collidedObjects.Add(hit.collider.gameObject);
            }
        }

        if (collidedObjects.Count >= 0)
        {
            collided = true;
        }
        else
        {
            collided = false;
        }

        return collidedObjects.ToArray();
    }

    public bool CheckCollisionPlayer()
    {
        RaycastHit2D hit;

        foreach (Vector4 item in rayVectors)
        {
            hit = GetHit(item, JUMP_DETECTION_LENGTH);
            if (hit.collider != null)
            {
                if(hit.collider.gameObject.layer == 4)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private RaycastHit2D GetHit(Vector4 pos, float length) => Physics2D.Raycast(new Vector2(pos.x, pos.y), new Vector2(pos.z, pos.w), length);

    private void OnDrawGizmosSelected()
    {
        rayVectors = GenerateRays();
        foreach (Vector4 item in rayVectors)
        {
            Gizmos.DrawLine(new Vector2(item.x, item.y), new Vector2(item.z, item.w));
        }
    }
}
