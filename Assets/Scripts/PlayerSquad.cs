using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerSquad : MonoBehaviour
{

    [Header("References")]
    public GameObject clonePrefab;
    private PlayerShooting playerShooting;
    public GameManager gameManager;


    [Header("Grid")]
    public int rows = 5;
    public int columns = 5;
    public float spacing;
    public Vector2 startPosition = new Vector2(-1.5f, 1f);
    Vector2[] positions;
    [HideInInspector] public GameObject[] clones;


    [Header("Gate Settings")]
    public float gateCooldown = 2f;
    private bool isCooldownActive = false;



    [Header("Squad Settings")]
    public int squadCount = 10;
    void Awake()
    {
        playerShooting = GetComponent<PlayerShooting>();
        CreateGrid();
        CreateClones();
        UpdateClones();
        UpdateSquadCountText();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gate") && !isCooldownActive)
        {
            Gate gate = other.GetComponent<Gate>();
            gate.ChangeSquadCount(this);
            StartCoroutine(GateCooldown());
        }
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
            clone.transform.localScale = Vector3.one; //yaratilan clone un scale i liderle ayni olsun.
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
        playerShooting.UpdateBulletScale(); //squadcount a bakarak bulletscale kontrol ediliyor.
        UpdateClones();
        UpdateSquadCountText();

        if (squadCount < 1) gameManager.GameOver();
    }
    public void UpdateSquadCountText()
    {
        if (gameManager.squadCountText != null)
        {
            gameManager.squadCountText.text = squadCount.ToString();
        }
    }

    IEnumerator GateCooldown()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(gateCooldown);
        isCooldownActive = false;
        
    }

    
}
