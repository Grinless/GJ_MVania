using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerWallChecker))]
public class PlayerWallJumpController : MonoBehaviour
{
    PlayerInput _input;
    PlayerWallChecker _checker;

    void Start()
    {
        _input = gameObject.GetComponent<PlayerController>().input;
        _checker = gameObject.GetComponent<PlayerWallChecker>();
    }

    void Update()
    {
        if (!_checker.OnWall)
            return;


        if (_input.jumpKey.KeyDown())
        {
            Debug.Log("Wall Jump Key Pressed!"); 

            
        }
    }
}


