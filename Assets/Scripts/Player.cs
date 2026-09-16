using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    //player clonelanmasi belki array izgarasi olusturularak cozulebilir.
    //player in scale i yuzde 25 azaltilir , 10 adet nokta olusturulur belki de sirasiyla nerelerde olusturulacagini
    // da ayarlayabiliriz. Bu sekilde playerin clone sayisi artar ve squadCount ile baglantili olur.
    
    
    
    [Header("Player")]
    public Rigidbody2D playerRb;
    public int squadCount = 10;

    [Header("Bullet")]
    public float bulletSpawnInterval = 0.5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float baseBulletScale = 1f;
    private float bulletScale;

    

    [Header("Swipe Movement")]
    public float swipeSpeed;
    float target = -1.5f;
    Vector2 touchStartPos;

    [Header("UI")]
    public TextMeshPro squadCountText;

    
    void Start()
    {

        playerRb = GetComponent<Rigidbody2D>();
        UpdateBulletScale();
        StartCoroutine(ShootInterval());
        UpdateSquadCountText();
    }

    void Update()
    {
        //shoot metodunu cagir.
        Movement();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.CompareTag("Enemy"))
        {
            squadCount /= 2;
        }*/
        if (other.CompareTag("Gate"))
        {
            Gate gate = other.GetComponent<Gate>();
            gate.ChangeSquadCount(this);
        }
    }

    public void Movement()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                touchStartPos = t.position;
            }
            if (t.phase == TouchPhase.Ended)
            {
                float swipeDistance = t.position.x - touchStartPos.x;
                if (swipeDistance > 50) target = 1.5f;
                if (swipeDistance < -50) target = -1.5f;
            }
            //ilk once touch baslangic noktasini kaydet. sonra touch ended oldugunda swipe mesafesini hesapla ve targeti belirle.
        }

        float x = Mathf.MoveTowards(transform.position.x, target, swipeSpeed * Time.deltaTime);
        transform.position = new Vector3(x, transform.position.y, 0);
        //belirledigimiz hizda playerin x pozisyonunu targete dogru gercek zamanli hareket ettir.
    }

    // squadCount'u degistiren her sey buradan gecmeli.
    public void SetSquad(int newCount)
    {
        squadCount = Mathf.Max(newCount, 0);
        UpdateBulletScale();
        UpdateSquadCountText();

        if (squadCount < 1) GameOver();
    }

    public void UpdateSquadCountText()
    {
        if (squadCountText != null)
        {
            squadCountText.text = squadCount.ToString();
        }
    }

    void BulletSpawn()
    {
        GameObject b = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        b.transform.localScale = bulletPrefab.transform.localScale * bulletScale;
    }

    IEnumerator ShootInterval()
    {
        while (true)
        {
            BulletSpawn();
            yield return new WaitForSeconds(bulletSpawnInterval);
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
        // Burada game over paneli acilacak.
    }


    void UpdateBulletScale()
    {
        float multiplier;
        if(squadCount >= 50) multiplier = 2.0f;
        else if(squadCount >= 25) multiplier = 1.5f;
        else if(squadCount >= 10) multiplier = 1.3f;
        else multiplier = 1.0f;

        bulletScale = baseBulletScale * multiplier;
    }



}