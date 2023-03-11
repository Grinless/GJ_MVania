using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for monitoring ground beneath the player. 
/// </summary>
[System.Serializable]
public class PlayerGroundChecker : MultiRayChecker
{
    private const float JUMP_DETECTION_DIST = .75f;
    private const float JUMP_DETECTION_LENGTH = 0.25f;
    private const int DIVISIONS = 5;
    private const float RAYCAST_WIDTH = 1.2f;

    public bool grounded;

    public bool Grounded => grounded = CheckCollision(RAYCAST_WIDTH, DIVISIONS, JUMP_DETECTION_DIST);

    public override Vector2 Start
    {
        get => new Vector2(transform.position.x, transform.position.y - JUMP_DETECTION_DIST);
    }

    public override Vector2 End
    {
        get => new Vector2(transform.position.x, transform.position.y - (JUMP_DETECTION_DIST + JUMP_DETECTION_LENGTH));
    }

#if DEBUG

    private void OnDrawGizmosSelected()
    {
        DrawGizmos(RAYCAST_WIDTH, DIVISIONS, JUMP_DETECTION_DIST);
    }

#endif


}
