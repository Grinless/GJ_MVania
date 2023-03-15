using UnityEngine;

public class RoomDisabler :MonoBehaviour
{
    public GameObject hubObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
            hubObj.SetActive(false);
    }
}