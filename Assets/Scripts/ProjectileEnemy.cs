using UnityEngine;
using System.Collections;

public class ProjectileEnemy : Enemy
{
    public GameObject enemyMissilePrefab;
    public float shootInterval = 2f;

    private bool hasArrived = false;
    public float arrivalPoint = 6f;


    IEnumerator ShootInterval()
    {
        while(true)
        {
            yield return new WaitForSeconds(shootInterval);
            Instantiate(enemyMissilePrefab, transform.position, Quaternion.identity);
        }
        
    }

    protected override void Move()
    {
        
        if (transform.position.y <= arrivalPoint)
        {
            if(!hasArrived)
            {
                hasArrived = true;
                StartCoroutine(ShootInterval());
            }
            
        }
        else
        {
            base.Move();
        }
    }


}
