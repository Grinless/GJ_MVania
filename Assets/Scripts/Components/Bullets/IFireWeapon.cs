using UnityEngine;

public interface IFireWeapon
{
    void FireWeapon(Vector3 position, Vector3 direction, out float cooldown);
}
