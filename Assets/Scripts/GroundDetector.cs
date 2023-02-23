using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float JUMP_DETECTION_DIST = .5f;
    [SerializeField] private float JUMP_DETECTION_LENGTH = 0.25f;
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
            print("Collider is not null.");

            if (hit.collider.gameObject != gameObject)
            {
                print("Collider is detecting non-originating object.");
                collisionResult = hit.collider.gameObject;

                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {

        RaycastHit2D hit2D = Physics2D.Raycast(Start, End);

        if (!hit2D.collider.gameObject.layer.Equals("Player"))
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.white;

        Gizmos.DrawLine(Start, End);

        Gizmos.DrawCube(Start, new Vector3(0.05f, 0.05f, 0.05f));
        Gizmos.DrawCube(End, new Vector3(0.05f, 0.05f, 0.05f));
    }
}