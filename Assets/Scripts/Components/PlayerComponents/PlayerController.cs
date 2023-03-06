using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovementController _pMoveCont;
    PlayerJumpController _pJumpCont;

    public PlayerHealth health = new PlayerHealth();
    public PlayerMovementData movementData = new PlayerMovementData();
    public PlayerInput input = new PlayerInput();
    public List<HeavyWeaponTypeData> heavyWeapons = new List<HeavyWeaponTypeData>();
    public List<NormalWeaponTypeData> normalWeapons = new List<NormalWeaponTypeData>();

    public static PlayerController instance;

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
        instance = this;

        _pMoveCont = gameObject.AddComponent<PlayerMovementController>();
        _pJumpCont = gameObject.GetComponent<PlayerJumpController>();
    }

    public void Start()
    {
        _pJumpCont.Setup(
            ref input,
            ref movementData,
            gameObject.GetComponent<Rigidbody2D>()
            );
    }
}
