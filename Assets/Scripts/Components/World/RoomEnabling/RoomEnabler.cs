using UnityEngine;

public class RoomEnabler : MonoBehaviour
{
    public GameObject hubObj;
    public bool startEnabled = false;

    private void Awake()
    {
        if(startEnabled)
            hubObj.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
            hubObj.SetActive(true);
    }
}
