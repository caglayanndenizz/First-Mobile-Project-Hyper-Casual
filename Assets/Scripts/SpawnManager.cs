using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject gatePrefab;
    public GameObject enemyPrefab;

    public PlayerSquad squad;
    public Enemy enemy;

    [Header("Values")]
    public float enemySpawnInterval = 8f;
    public float gateSpawnInterval = 5f;
    public float baseEnemyHealth = 50f;
    public float healthPerSquad = 10f;


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
        Transform spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        float health = baseEnemyHealth + squad.squadCount * healthPerSquad;
        enemy.GetComponent<Enemy>().SetHealth(health);
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
