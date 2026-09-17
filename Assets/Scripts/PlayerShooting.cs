using System.Collections;
using UnityEngine;


public class PlayerShooting : MonoBehaviour
{
    private PlayerSquad squad;
    public float bulletSpawnInterval = 0.5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float baseBulletDamage = 10f;
    public float baseBulletScale = 1f;
    private float bulletScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        squad = GetComponent<PlayerSquad>();
        UpdateBulletScale();
        
        StartCoroutine(ShootInterval());
    }

    // Update is called once per frame
    void Update()
    {

    }


    void Shoot(Vector3 position)
    {
        GameObject b = Instantiate(bulletPrefab, position, Quaternion.identity);
        b.transform.localScale = bulletPrefab.transform.localScale * bulletScale;
        b.GetComponent<Bullet>().bulletDamage = Mathf.RoundToInt(baseBulletDamage * bulletScale);
    }

    void BulletSpawn()
    {
        Shoot(bulletSpawnPoint.position);
        for (int i = 0; i < squad.clones.Length; i++)
        {
            if (squad.clones[i].activeSelf)
            {
                Shoot(squad.clones[i].transform.position);
            }
        }
    }
    


    IEnumerator ShootInterval()
    {
        while (true)
        {
            BulletSpawn();
            yield return new WaitForSeconds(bulletSpawnInterval);
        }
    }

   


    public void UpdateBulletScale()
    {
        float multiplier;
        if (squad.squadCount >= 50) multiplier = 2.0f;
        else if (squad.squadCount >= 25) multiplier = 1.5f;
        else if (squad.squadCount >= 10) multiplier = 1.3f;
        else multiplier = 1.0f;

        bulletScale = baseBulletScale * multiplier;
    }
}
