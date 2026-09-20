using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed = 1.5f;
    public float currentHealth;
    public float maxHealth = 100f;

    //public float threshold = -5f;
    private GameObject threshold;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        threshold = GameObject.FindGameObjectWithTag("Threshold");
    }
    protected virtual void Update()
    {
        Move();
        ThresholdPass();
        
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.down * enemySpeed * Time.deltaTime);

        if (transform.position.y < -10)
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
    
    public void ThresholdPass()
    {

        if (gameObject.transform.position.y <= threshold.transform.position.y - 1)
        {
            Time.timeScale = 0f;
            Debug.Log("The enemy has crossed the threshold! Game Over");
        }
    }

   

    
}
