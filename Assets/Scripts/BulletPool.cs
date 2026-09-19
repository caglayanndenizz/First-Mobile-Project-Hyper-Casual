using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;
    public GameObject bulletPrefab;
    private ObjectPool<GameObject> pool;
    void Awake()
    {
        instance = this;
        pool = new ObjectPool<GameObject>(CreateBullet, GetBullet, ReleaseBullet, DestroyBullet);
    }
    GameObject CreateBullet()
    {
        //yoktan bir sey yaratilirken gameobject metodu kullanilmali.
        return Instantiate(bulletPrefab);
    }

    void GetBullet(GameObject bullet)
    {
        bullet.SetActive(true);
    }

    void ReleaseBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    void DestroyBullet(GameObject bullet)
    {
        Destroy(bullet);
    }

    public GameObject SpawnBullet(Vector3 position)
    {
        GameObject bullet = pool.Get();
        bullet.transform.position = position;
        bullet.transform.rotation = Quaternion.identity;
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        pool.Release(bullet);
    }
}
