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
        // Проверяем наличие объекта с тегом "Player" перед получением компонента Transform
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Transform>();
            StartCoroutine(SpawnObjects());
        }
        else
        {
            Debug.LogError("Player object not found! Make sure there is a GameObject with tag 'Player' in your scene.");
        }
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
        if (player == null)
        {
            Debug.LogError("Player transform is null. Cannot determine spawn position.");
            return Vector2.zero;
        }

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float distance = Random.Range(minSpawnRadius, maxSpawnRadius);
        return (Vector2)player.position + randomDirection * distance;
    }

    private GameObject GetRandomObject()
    {
        float randomValue = Random.value;

        if (randomValue < 0.1f)
        {
            return largeAsteroidPrefab;
        }
        else if (randomValue < 0.2f)
        {
            return largeAsteroidPrefab;
        }
        else if (randomValue < 0.75f)
        {
            return mediumAsteroidPrefab;
        }
        else if (randomValue < 0.85f)
        {
            return mediumAsteroidPrefab;
        }
        else if (randomValue < 0.95f)
        {
            return ufoPrefab;
        }
        else
        {
            return smallAsteroidPrefab;
        }
    }
}
