using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for spawning items on enemy death. 
/// </summary>
public class DropOnDeath : MonoBehaviour
{
    public List<GameObject> drops = new List<GameObject>(); 
    

    public void Activate()
    {
        GameObject obj; 

        foreach (GameObject item in drops)
        {
            obj = GameObject.Instantiate(item);
            obj.transform.position = gameObject.transform.position;
        }
    }
}