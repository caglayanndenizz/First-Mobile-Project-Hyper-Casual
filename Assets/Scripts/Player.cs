using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{



    [Header("Player")]
    public Rigidbody2D playerRb;
    public int squadCount = 10;


    [Header("Squad")]
    public GameObject clonePrefab;

    public int rows = 5;
    public int columns = 5;
    public float spacing = 2f;
    public Vector2 startPosition = new Vector2(-1.5f, 1f);

    Vector2[] positions;
    GameObject[] clones;

    [Header("Bullet")]
    public float bulletSpawnInterval = 0.5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float baseBulletDamage = 10f;
    public float baseBulletScale = 1f;
    private float bulletScale;



    [Header("Swipe Movement")]
    public float swipeSpeed;
    float target = 1.5f;
    Vector2 touchStartPos;

    [Header("UI")]
    public TextMeshPro squadCountText;


    void Start()
    {

        playerRb = GetComponent<Rigidbody2D>();

        CreateGrid();
        CreateClones();
        UpdateBulletScale();
        UpdateClones();
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

    void CreateGrid() //sadece yumurta kolisi gorevinde.
    //yumurtalar farkli metodda yerlestirilcek.
    {
        positions = new Vector2[rows * columns];

        int index = 0; // index in isi o an kacinci gozde oldugunu bilmek.

        //dongunun disinda oldugu zaman 0 olarak belirlenir ve surekli artar.
        //disinda olmazsa her turda tekrar 0 olur.


        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float x = startPosition.x + col * spacing;
                float y = startPosition.y - row * spacing;
                //koordinat sistemine gore asagi inilmek istediginde - olacak cunku sutunlari saga dogru
                //yaratiyoruz satirlari ise asagiya dogru.

                positions[index] = new Vector3(x, y, 0);
                index++;
            }
        }
    }

    void CreateClones()
    {
        clones = new GameObject[positions.Length];
        //olusturulan nokta sayisi kadar clone yaratilir.

        for (int i = 0; i < positions.Length; i++)
        {
            GameObject clone = Instantiate(clonePrefab, transform); //transform = lider player
            clone.transform.localPosition = positions[i]; //yaratilan clone un pozisyonunu griddeki siradaki yere yerlestir.
            clone.transform.localScale = Vector2.one; //yaratilan clone un scale i liderle ayni olsun.
            clone.SetActive(false); //simdilik aktif olmasin.
            clones[i] = clone; //yaratilan clone u grid e yerlestir.
        }

    }
    
    void UpdateClones()
    {
        for (int i = 0; i < clones.Length; i++)
        {
            clones[i].SetActive(i < squadCount - 1);
        }
    }

    public void SetSquad(int newCount)
    {
        squadCount = Mathf.Max(newCount, 0);
        UpdateBulletScale(); //squadcount a bakarak bulletscale kontrol ediliyor.
        UpdateClones();
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

    void Shoot(Vector3 position)
    {
        GameObject b = Instantiate(bulletPrefab, position, Quaternion.identity);
        b.transform.localScale = bulletPrefab.transform.localScale * bulletScale;
        b.GetComponent<Bullet>().bulletDamage = Mathf.RoundToInt(baseBulletDamage * bulletScale);
    }

    void BulletSpawn()
    {
        Shoot(bulletSpawnPoint.position);
        for (int i = 0; i < clones.Length; i++)
        {
            if (clones[i].activeSelf)
            {
                Shoot(clones[i].transform.position);
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

    void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
        // Burada game over paneli acilacak.
    }


    public void UpdateBulletScale()
    {
        float multiplier;
        if (squadCount >= 50) multiplier = 2.0f;
        else if (squadCount >= 25) multiplier = 1.5f;
        else if (squadCount >= 10) multiplier = 1.3f;
        else multiplier = 1.0f;

        bulletScale = baseBulletScale * multiplier;
    }

    
    



}