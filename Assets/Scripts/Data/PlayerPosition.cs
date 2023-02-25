using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

/// <summary>
/// Class responsible for saving players last position in the world. 
/// </summary>
public class PlayerPosition
{
    public float x;
    public float y;
    public float z;

    public PlayerPosition(Vector3 position)
    {
        this.x = position.x;
        this.y = position.y;
        this.z = position.z; 
    }

    public Vector3 GetPosition()
    {
        return new Vector3(x, y, z);
    }
}
