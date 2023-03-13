using System.Collections;
using UnityEngine;

public class MassChase_Startup : MonoBehaviour
{
    public GameObject bossPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != 6)
            return;

        GameObject.Instantiate(bossPrefab);
    }
}