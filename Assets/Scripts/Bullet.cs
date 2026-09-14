using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletDamage = 10f;
    public float lifetime = 4f;
    public float spawnInterval;


    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        BulletMovement();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }

    void BulletMovement()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    
}
