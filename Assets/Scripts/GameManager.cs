using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshPro squadCountText; //bunu playersquad a tasiyabiliriz. Simdilik kalsin.

     public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
        // Burada game over paneli acilacak UI manager scriptinden referans verip.
    }
}