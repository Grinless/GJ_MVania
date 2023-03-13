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
    public Vector2 target;
    public bool active = false; 

    public void Setup(GameObject obj)
    {
        obj.transform.position = waypoints[0].Position;
        target = waypoints[index].Position;
    }

    public void UpdateWaypoints(Vector2 currentPosition) => UpdatePoint(currentPosition);

    public void LateUpdate(GameObject obj, float speed)
    {
        if (!active)
            return;

        obj.transform.position = 
            Vector3.MoveTowards(obj.transform.position, target, speed * Time.deltaTime);
    }

    private void UpdatePoint(Vector2 currentPosition)
    {
        if (!active)
            return;

        if ((currentPosition == target && index < waypoints.Length - 1))
        {
            index++;
            target = waypoints[index].Position;
        }
    }

    #region Draw Points
    public void DrawGizmos()
    {
        if(waypoints.Length == 0) 
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(waypoints[0].Position, 0.3f);

        Gizmos.color = Color.red;

        for (int i = 1; i < waypoints.Length; i++)
        {
            Gizmos.DrawSphere(waypoints[i].Position, 0.3f);
        }

        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].Position, waypoints[i + 1].Position);
        }
    }

    #endregion
}
