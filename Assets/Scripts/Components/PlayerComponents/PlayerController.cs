using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PMovementController _pMoveCont;
    PJumpController _pJumpCont;

    public PlayerHealth health = new PlayerHealth();
    public PlayerMovementData movementData = new PlayerMovementData();
    public PlayerInput input = new PlayerInput();
    public List<HeavyWeaponTypeData> heavyWeapons = new List<HeavyWeaponTypeData>();
    public List<NormalWeaponTypeData> normalWeapons = new List<NormalWeaponTypeData>();

    public PlayerHealth PlayerHealthData
    {
        get { return health; }
    }

    public PlayerMovementData MovementData
    {
        get { return movementData; }
    }

    public PlayerInput Input
    {
        get { return input; }
    }

    public void Awake()
    {
        _pMoveCont = gameObject.AddComponent<PMovementController>();
        _pJumpCont = gameObject.AddComponent<PJumpController>();
    }

    public void Start()
    {
        _pMoveCont.Setup(
            ref input,
            ref movementData,
            gameObject.GetComponent<Rigidbody2D>()
            );

        _pJumpCont.Setup(
            ref input,
            ref movementData,
            gameObject.GetComponent<Rigidbody2D>()
            );
    }
}
