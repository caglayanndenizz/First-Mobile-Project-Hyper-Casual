using UnityEngine;
using TMPro;
using System.Linq.Expressions;


public enum GateType { Add , Subtract , Multiply , Divide }
public class Gate : MonoBehaviour
{

    public GateType gateType;
    public int gateValue;
    public TextMeshPro typeValueText;


    void Start()
    {
        RandomizeGateType();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null)
        {
            switch (gateType)
            {
                case GateType.Add:
                    p.squadCount += gateValue;
                    break;
                case GateType.Subtract:
                    p.squadCount -= gateValue;
                    break;
                case GateType.Multiply:
                    p.squadCount *= gateValue;
                    break;
                case GateType.Divide:
                    p.squadCount /= gateValue;
                    break;
            }
        }

        if (p.squadCount < 1)
        {
            p.squadCount = 0;
            Debug.Log("Game Over");
            Destroy(gameObject);

            //bu kisim calismiyor. Sonucu bir degiskende toplayip sonra player a gondermemiz lazim!!!!!!!
        }

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

}
