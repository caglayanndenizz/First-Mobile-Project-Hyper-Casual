using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 2f;

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector2.down * projectileSpeed * Time.deltaTime);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy();
        }
    }
    
    void Destroy()
    {
        Destroy(gameObject);
    }
}
