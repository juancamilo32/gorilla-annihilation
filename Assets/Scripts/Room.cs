using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    [SerializeField]
    List<Vector3> enemySpawnPoints = new List<Vector3>();

    [SerializeField]
    GameObject enemyPrefab;

    bool roomCleared = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !roomCleared)
        {
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        int numberOfEnemies = Random.Range(2, 5);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int pos = Random.Range(0, enemySpawnPoints.Count);
            Vector3 spawnPoint = transform.position + enemySpawnPoints[pos];
            Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            enemySpawnPoints.Remove(enemySpawnPoints[pos]);
        }
        roomCleared = true;
    }

}
