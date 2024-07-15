using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // Префаб объекта, который нужно спавнить
    public Transform player; // Ссылка на объект игрока
    public float minSpawnRadius = 5f; // Минимальный радиус, на котором объекты будут спавниться
    public float maxSpawnRadius = 10f; // Максимальный радиус, на котором объекты будут спавниться
    public float spawnInterval = 2f; // Интервал между спавнами

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            Vector2 spawnPosition = GetRandomPosition();
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
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
}