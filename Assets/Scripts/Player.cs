using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TextMeshPro squadCountText;
    public Rigidbody2D playerRb;
    public int squadCount = 10;
    public GameObject bulletPrefab;

    [Header("Swipe Movement")]
    public float swipeSpeed;
    float target = -1.5f;
    Vector2 touchStartPos;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
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

    public void Shoot()
    {
        //bullet scriptine baglanti ve instantiate.
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

    void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
        // Burada game over paneli acilacak.
    }
}