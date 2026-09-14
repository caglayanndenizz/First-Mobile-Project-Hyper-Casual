using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * enemySpeed * Time.deltaTime);
    }
}
