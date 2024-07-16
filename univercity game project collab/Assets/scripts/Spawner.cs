using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] int difficulty;
    public GameObject largeAsteroidPrefab;
    public GameObject mediumAsteroidPrefab;
    public GameObject smallAsteroidPrefab;
    public GameObject ufoPrefab;
    private Transform player;
    public float minSpawnRadius = 5f;
    public float maxSpawnRadius = 10f;
    [SerializeField] float spawnInterval;
    private float chanceLarge, chanceMedium, chanceUFO;

    private void Start()
    {
        difficulty = PlayerPrefs.GetInt("diff", 1);
        ConfigureSpawnSettings();
        
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Transform>();
            StartCoroutine(SpawnObjects());
        }
        else
        {
            Debug.LogError("Player object not found! Ensure there is a GameObject with tag 'Player' in your scene.");
        }
    }

    private void ConfigureSpawnSettings()
    {
        switch (difficulty)
        {
            case 1:
                spawnInterval = 2.5f;
                chanceLarge = 0.05f;
                chanceMedium = 0.35f;
                chanceUFO = 0.0f; 
                break;
            case 2:
                spawnInterval = 2f;
                chanceLarge = 0.4f;
                chanceMedium = 0.45f;
                chanceUFO = 0.1f; 
                break;
            case 3:
                spawnInterval = 1.5f;
                chanceLarge = 0.8f;
                chanceMedium = 0.9f;
                chanceUFO = 0.2f; 
                break;
            default:
                Debug.LogWarning("Invalid difficulty level. Defaulting to difficulty 1.");
                ConfigureSpawnSettings();
                break;
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

        if (randomValue < chanceLarge)
        {
            return largeAsteroidPrefab;
        }
        else if (randomValue < chanceMedium)
        {
            return mediumAsteroidPrefab;
        }
        else if (randomValue < chanceMedium + chanceUFO && difficulty >= 2)
        {
            return ufoPrefab;
        }
        else
        {
            return smallAsteroidPrefab;
        }
    }
}
