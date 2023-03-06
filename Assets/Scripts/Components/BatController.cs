using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BatData
{
    public float distance = -1;
    public float width = 1;
    public float offset = -1;
    public float initalAngle = 0;
    public float maxAngle = 30;
    public int numberOfRays = 2;
}

public class BatController : MonoBehaviour
{
    ConeCaster _coneCaster;

    public BatData data = new BatData();

    //public float distance = -1;
    //public float width = 1;
    //public float offset = -1;
    //public float initalAngle = 0;
    //public float maxAngle = 30;
    //public int numberOfRays = 2;

    //public Vector2 Position
    //{
    //    get => new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + offset);
    //}

    //public float InitalAngleRadians
    //{
    //    get => initalAngle * Mathf.Deg2Rad;
    //}

    //public float MaxAngleRadians
    //{
    //    get => maxAngle * Mathf.Deg2Rad;
    //}

    void Start()
    {
        //Calculate Rays.
        //GenerateRays();

        _coneCaster = new ConeCaster(ref data, transform);
    }

    //private void GenerateRays()
    //{
    //    List<VectorContainer> list = new List<VectorContainer>();
    //    float angleIncrement = MaxAngleRadians / (numberOfRays * 2);
    //    float currentAngle = (0 * Mathf.Deg2Rad) + angleIncrement;
    //    float currentAngleNeg = (0 * Mathf.Deg2Rad) - angleIncrement;


    //    for (int i = 0; i < numberOfRays; i++)
    //    {

    //        list.Add(CreateRayVecs(Position, currentAngle));
    //        list.Add(CreateRayVecs(Position, currentAngleNeg));
    //        currentAngle += angleIncrement;
    //        currentAngleNeg -= angleIncrement;
    //    }

    //    rays = list.ToArray();
    //}

    //private VectorContainer CreateRayVecs(Vector2 position, float radian)
    //{
    //    return new VectorContainer(
    //                position,
    //                new Vector2(
    //                    position.x + Mathf.Cos(InitalAngleRadians + radian) * Mathf.Rad2Deg,
    //                    position.y + Mathf.Sin(InitalAngleRadians + radian) * Mathf.Rad2Deg
    //                    ).normalized);
    //}

    void Update()
    {
        _coneCaster.CheckCollision();
    }

    private void OnDrawGizmos()
    {
        if (_coneCaster == null)
        {
            _coneCaster = new ConeCaster(ref data, transform);
        }

        //_coneCaster.SetInitalAngle(initalAngle);

        _coneCaster.OnGizmosDraw();

        //GenerateRays();

        //Gizmos.color = Color.white;

        //foreach (VectorContainer item in rays)
        //{
        //    Gizmos.DrawRay(new Ray(item.Start, item.End * distance));
        //}

        //Gizmos.color = Color.red;

        //Gizmos.DrawLine(rays[0].Start, rays[0].End);
        //Gizmos.DrawLine(rays[rays.Length -1].Start, rays[rays.Length -1].End);
    }

    bool CheckForPlayer()
    {
        return false;
    }
}