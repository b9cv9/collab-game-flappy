using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject largeAsteroidPrefab;
    public GameObject mediumAsteroidPrefab;
    public GameObject smallAsteroidPrefab;
    public GameObject ufoPrefab;
    private Transform player;
    public float minSpawnRadius = 5f;
    public float maxSpawnRadius = 10f;
    public float spawnInterval = 2f;

    private void Start()
    {
        StartCoroutine(SpawnObjects());
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            Vector2 spawnPosition = GetRandomPosition();
            GameObject objectToSpawn = GetRandomObject();
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float distance = Random.Range(minSpawnRadius, maxSpawnRadius);
        Vector2 spawnPosition = (Vector2)player.position + randomDirection * distance;
        return spawnPosition;
    }

    private GameObject GetRandomObject()
    {
        float randomValue = Random.value;

        if (randomValue < 0.1f)
        {
            return largeAsteroidPrefab; // 10% large asteroids that break into two medium ones
        }
        else if (randomValue < 0.2f)
        {
            return largeAsteroidPrefab; // 10% large asteroids that break into medium and then small
        }
        else if (randomValue < 0.75f)
        {
            return mediumAsteroidPrefab; // 55% medium asteroids that break into two small ones
        }
        else if (randomValue < 0.85f)
        {
            return mediumAsteroidPrefab; // 10% medium asteroids that break into medium
        }
        else if (randomValue < 0.95f)
        {
            return ufoPrefab; // 15% UFOs
        }
        else
        {
            return smallAsteroidPrefab; // 10% small asteroids
        }
    }
}
