using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletDamage = 10f;
    public float bulletIncreasedDamage;
    public float lifetime = 5f;
    public float spawnInterval;
    private float timer;
    private bool isReturned;

    void OnEnable()
    {
        timer = 0f;
        isReturned = false;
    }

    void Update()
    {
        BulletMovement();

        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            ReturnToPool();
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(bulletDamage);
            BulletPool.instance.ReturnBullet(gameObject);
        }
    }

    void BulletMovement()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    void ReturnToPool()
    {
        if (isReturned) return;
        isReturned = true;
        BulletPool.instance.ReturnBullet(gameObject);
    }

    
}
