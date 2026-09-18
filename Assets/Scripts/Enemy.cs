using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed = 1.5f;
    public float currentHealth;
    public float maxHealth = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected virtual void Update()
    {
        Move();
    }

     protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.down * enemySpeed * Time.deltaTime);

        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetHealth(float newHealth)
    {
        maxHealth = newHealth;
        currentHealth = newHealth;
    }

   

    
}
