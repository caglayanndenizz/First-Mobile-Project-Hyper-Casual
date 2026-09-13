using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody2D playerRb;
    public int squadCount = 1;
    public GameObject gate;
    public GameObject bulletPrefab;

    [Header("Swipe Movement")]
    public float swipeSpeed;
    float target = -1.5f;
    Vector2 touchStartPos;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //shoot metodunu cagir.
        Movement();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            squadCount /= 2;
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



}
