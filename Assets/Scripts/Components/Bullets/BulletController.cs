using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector3 _direction;
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);
    }

    private void Update()
    {
        rb.AddForce(_direction, ForceMode2D.Impulse);
    }
}
