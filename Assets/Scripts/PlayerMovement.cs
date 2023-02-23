using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //private Rigidbody2D body2D;
    //private Vector2 movement;
    //public PlayerInput input = new PlayerInput();
    //public PlayerMovementData data = new PlayerMovementData();

    //private Vector2 GetGroundCenterPos
    //{
    //    get
    //    {
    //        return new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - data.groundDetectionDist);
    //    }
    //}

    //public void Start()
    //{
    //    body2D = GetComponent<Rigidbody2D>();
    //}

    //public void Update()
    //{
    //    movement = Vector2.zero;

    //    //Update input collection. 
    //    if (Input.GetKey(input.leftKey))
    //    {
    //        movement.x = -1;
    //    }
    //    else if (Input.GetKey(input.rightKey))
    //    {
    //        movement.x = 1;
    //    }
    //    else
    //    {
    //        if (movement.x != 0)
    //        {
    //            movement = body2D.velocity;
    //            if (movement.x < 0)
    //            {
    //                movement.x += movement.x * data.runSlowdown;
    //                if (movement.x > 0)
    //                {
    //                    movement.x = 0;
    //                }
    //            }
    //            else if (movement.x > 0)
    //            {
    //                movement.x -= movement.x * data.runSlowdown;
    //                if (movement.x < 0)
    //                {
    //                    movement.x = 0;
    //                }
    //            }
    //        }
    //    }
    //    JumpRaycastCheck();

    //    if (Input.GetKeyUp(input.jumpKey) && grounded)
    //    {
    //        movement.y += data.jumpDistance;
    //    }

    //}

    //private void LateUpdate()
    //{
    //    body2D.velocity = (movement * data.runSpeed);

    //}

    //public bool grounded; 

    //private void JumpRaycastCheck()
    //{
    //    Vector2 position = gameObject.transform.position;
    //    position.y -= 1f;
    //    RaycastHit2D hit = Physics2D.Raycast(GetGroundCenterPos, GetGroundCenterPos + (Vector2.down / 4));
    //    if(hit.collider.gameObject != this.gameObject)
    //    {
    //        grounded = true;
    //    }
    //    else grounded = false;
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    if (grounded)
    //    {
    //        Gizmos.color = Color.white;
    //    }
    //    else
    //    {
    //        Gizmos.color = Color.red;
    //    }
    //    Vector2 position = gameObject.transform.position;
    //    position.y -= 0.45f;
    //    Gizmos.DrawLine(GetGroundCenterPos, GetGroundCenterPos + (Vector2.down / 4));
    //}
}
