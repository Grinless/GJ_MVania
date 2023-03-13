using UnityEngine;

[System.Serializable]
public class WayPoint
{
    public Vector2 Position = new Vector2();
    public float delayTime;
    public bool delay;
}

[System.Serializable]
public class WaypointSystem
{
    private int index = 1;
    public WayPoint[] waypoints;
    public WayPoint target;
    float currentDelayTime;
    public bool pathEnded = false;

    public Vector2 TargetPos
    {
        get { return target.Position; }
    }

    public void Setup(GameObject obj)
    {
        obj.transform.position = waypoints[0].Position;
        target = waypoints[index];
    }

    public void UpdateWaypoints(Vector2 currentPosition) => UpdatePoint(currentPosition);

    public void LateUpdate(GameObject obj, float speed)
    {
        if (TargetPos != (Vector2)obj.transform.position)
        {
            obj.transform.position =
                Vector3.MoveTowards(obj.transform.position, TargetPos, speed * Time.deltaTime);
        }

        if (currentDelayTime > 0)
        {
            currentDelayTime -= Time.deltaTime;
            return;
        }
    }

    public void LateUpdate(GameObject obj, Rigidbody2D body2D, float speed)
    {
        if (TargetPos != (Vector2)obj.transform.position)
        {
            Vector2 direction;
            if (Vector3.Distance(obj.transform.position, TargetPos) > 0.5f)
            {
                direction = TargetPos - (Vector2)obj.transform.position;
                body2D.AddRelativeForce(direction.normalized * speed * Time.deltaTime, ForceMode2D.Force);
            }
        }

        if (currentDelayTime > 0)
        {
            currentDelayTime -= Time.deltaTime;
            return;
        }
    }

    private void UpdatePoint(Vector2 currentPosition)
    {
        if (currentDelayTime > 0)
            return;
        if ((currentPosition == TargetPos && index < waypoints.Length - 1))
        {
            index++;
            target = waypoints[index];
            currentDelayTime = target.delayTime;
            return;
        }
        if (currentPosition == TargetPos && index == waypoints.Length - 1)
            pathEnded = true;
    }

    #region Draw Points
    public void DrawGizmos()
    {
        if (waypoints.Length == 0) //Do this to avoid drawing errors. 
            return;

        //Draw inital point. 
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(waypoints[0].Position, 0.3f);

        //Draw path points. 
        Gizmos.color = Color.red;
        for (int i = 1; i < waypoints.Length; i++)
        {
            Gizmos.DrawSphere(waypoints[i].Position, 0.3f);
        }

        //Draw path lines. 
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].Position, waypoints[i + 1].Position);
        }
    }

    #endregion
}
