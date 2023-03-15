using UnityEngine;

public class AreaNode : MonoBehaviour
{
    public Collider2D loadTrigger;
    public bool triggered = false;
    public GameObject loadable; 

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Occured: " + collision.gameObject.name);

        if (collision.gameObject.layer == 6)
        {
            triggered = true;
            Debug.Log("AreaNode: " + gameObject.name + " Triggered by: " + collision.gameObject.name); 
        }
    }
}