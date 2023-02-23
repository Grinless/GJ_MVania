using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GroundCheckerTester : MonoBehaviour
{
    public GroundDetector checker;
    public bool result = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        result = checker.Grounded;

        if (result)
        {
            if (checker.collisionResult != gameObject)
            {
                transform.localScale = new Vector3(1, 1, 0);
                return;
            }
        }
        transform.localScale = new Vector3(0.5f, 0.5f, 0);
    }
}
