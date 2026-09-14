using UnityEngine;
using TMPro;

public enum GateType { Add, Subtract, Multiply, Divide }

public class Gate : MonoBehaviour
{
    public GateType gateType;
    public int gateValue;
    public TextMeshPro typeValueText;

    [Header("Gate Color")]
    public Color[] colors;
    private SpriteRenderer spriteRenderer;

    [Header("Gate Speed")]
    [SerializeField] private float gateMovementSpeed;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        RandomizeGateType();
        SetRandomColor();
    }

    void Update()
    {
        GateMovement();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            Destroy(gameObject);
    }

    public void RandomizeGateType()
    {
        gateType = (GateType)Random.Range(0, 4);

        switch (gateType)
        {
            case GateType.Add:
                gateValue = Random.Range(2, 10);
                break;
            case GateType.Subtract:
                gateValue = Random.Range(2, 10);
                break;
            case GateType.Multiply:
                gateValue = Random.Range(2, 4);
                break;
            case GateType.Divide:
                gateValue = Random.Range(2, 4);
                break;
        }

        UpdateText();
    }

    public void UpdateText()
    {
        if (typeValueText == null) return;

        string symbol = "";
        switch (gateType)
        {
            case GateType.Add: symbol = "+"; break;
            case GateType.Subtract: symbol = "-"; break;
            case GateType.Multiply: symbol = "x"; break;
            case GateType.Divide: symbol = "/"; break;
        }
        typeValueText.text = symbol + gateValue;
    }

    void GateMovement()
    {
        transform.Translate(0, -gateMovementSpeed * Time.deltaTime, 0);
        if (transform.position.y < -10)
            Destroy(gameObject);
        //buraya pooler a geri donmesini yazacagiz fakat simdilik destroy olarak kalsin.
    }

    void SetRandomColor()
    {
        Color c = colors[Random.Range(0, colors.Length)];
        c.a = 1f;
        spriteRenderer.color = c;
    }

    public void ChangeSquadCount(Player player)
    {
        if (player == null) return;

        int result = player.squadCount;

        switch (gateType)
        {
            case GateType.Add:      result += gateValue; break;
            case GateType.Subtract: result -= gateValue; break;
            case GateType.Multiply: result *= gateValue; break;
            case GateType.Divide:   result /= gateValue; break;
        }

        player.SetSquad(result);
    }
}