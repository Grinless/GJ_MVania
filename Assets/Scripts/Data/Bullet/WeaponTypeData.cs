using UnityEngine;

[System.Serializable]
public abstract class WeaponTypeData : IFireWeapon
{
    public GameObject bulletPrefab;

    void IFireWeapon.FireWeapon(Vector3 position, Vector3 direction,
        float damage, out float cooldown)
    {
        GameObject newBullet = CreateAtPosition(bulletPrefab, position);
        Rigidbody2D rb = newBullet.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = (direction * GetVelocity());
        newBullet.AddComponent<BulletController>();
        newBullet.AddComponent<BulletDamageHandler>().damage = damage;
        cooldown = GetCooldown();
    }

    private GameObject CreateAtPosition(GameObject bulletPrefab, Vector3 position)
    {
        GameObject obj = GameObject.Instantiate<GameObject>(bulletPrefab);
        obj.transform.position = position;
        return obj;
    }

    protected abstract float GetVelocity();
    protected abstract float GetCooldown();
}
