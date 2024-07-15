using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;

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
    [SerializeField] float spawnInterval = 2f;
    private float chanceLarge, chanceMedium;

    private void Start()
    {
        difficulty = PlayerPrefs.GetInt("diff", 1);
        if (difficulty == 2)
        {
            spawnInterval = 3f;
            chanceLarge = 0.3f;
            chanceMedium = 0.45f;
        }
        else if (difficulty == 3)
        {
            spawnInterval = 2f;
            chanceLarge = 0.6f;
            chanceMedium = 0.85f;
        }
        else if (difficulty == 1)
        {
            spawnInterval = 3.5f;
            chanceLarge = 0.05f;
            chanceMedium = 0.35f;
        }
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

        if (randomValue < chanceLarge)
        {
            return largeAsteroidPrefab;
        }
        else if (randomValue < chanceMedium)
        {
            return mediumAsteroidPrefab;
        }
        else if (randomValue < 0.95f && difficulty == 3)
        {
            return ufoPrefab;
        }
        else
        {
            return smallAsteroidPrefab;
        }
    }
}
