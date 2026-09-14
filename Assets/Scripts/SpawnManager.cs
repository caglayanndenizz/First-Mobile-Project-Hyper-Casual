using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject gatePrefab;
    public GameObject enemyPrefab;

    [Header("Values")]
    public float enemySpawnInterval = 8f;
    public float gateSpawnInterval = 5f;


    [Header("Spawn Points")]
    public Transform[] enemySpawnPoints;
    public Transform gateSpawnPoint;


    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(GateSpawnRoutine());
    }

    void Update()
    {

    }

    void SpawnGate()
    {
        Instantiate(gatePrefab, gateSpawnPoint.position, Quaternion.identity);
    }

    void SpawnEnemy()
    {
        Transform transform = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(enemySpawnInterval);
        }
    }
    
    IEnumerator GateSpawnRoutine()
    {
        while (true)
        {
            SpawnGate();
            yield return new WaitForSeconds(gateSpawnInterval);
        }
    }
    
}
