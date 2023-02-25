using UnityEngine;

/// <summary>
/// Class responsible for monitoring ground beneath the player. 
/// </summary>
[System.Serializable]
public class GroundChecker : MonoBehaviour
{
    private const float JUMP_DETECTION_DIST = .75f;
    private const float JUMP_DETECTION_LENGTH = 0.4f;

#if DEBUG
    [SerializeField] private bool debug = true;
#endif

    public bool Grounded => CheckGround();
    public GameObject collisionResult { get; private set; }

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
        RaycastHit2D hit = Physics2D.Raycast(Start, End, JUMP_DETECTION_LENGTH);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject != gameObject)
            {
                collisionResult = hit.collider.gameObject;

                return true;
            }
        }
        return false;
    }

#if DEBUG
    private void OnDrawGizmosSelected()
    {
        if (!debug)
            return;
        RaycastHit2D hit2D = Physics2D.Raycast(Start, End);

        Gizmos.DrawLine(Start, End);

        Gizmos.DrawCube(Start, new Vector3(0.05f, 0.05f, 0.05f));
        Gizmos.DrawCube(End, new Vector3(0.05f, 0.05f, 0.05f));
        Gizmos.DrawCube(hit2D.point, new Vector3(0.05f, 0.05f, 0.05f));

    }
#endif
}
