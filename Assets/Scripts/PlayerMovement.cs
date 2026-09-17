using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float swipeSpeed;
    float target = 1.5f;
    Vector2 touchStartPos;


    void Update()
    {
        Movement();
    }
    
    void Movement()
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
