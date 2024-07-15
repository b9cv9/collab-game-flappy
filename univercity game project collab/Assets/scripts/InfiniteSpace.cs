using UnityEngine;
using System.Collections.Generic;

public class InfiniteSpace : MonoBehaviour
{
    public GameObject segmentPrefab; // Префаб сегмента карты
    public int segmentSize = 10; // Размер одного сегмента
    public int viewDistance = 3; // Расстояние видимости в сегментах

    private Transform player; // Трансформ игрока
    private Dictionary<Vector2Int, GameObject> segments = new Dictionary<Vector2Int, GameObject>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        UpdateSegments();
    }

    void Update()
    {
        UpdateSegments();
    }

    void UpdateSegments()
    {
        Vector2Int playerPosition = new Vector2Int(
            Mathf.FloorToInt(player.position.x / segmentSize),
            Mathf.FloorToInt(player.position.y / segmentSize)
        );

        for (int x = playerPosition.x - viewDistance; x <= playerPosition.x + viewDistance; x++)
        {
            for (int y = playerPosition.y - viewDistance; y <= playerPosition.y + viewDistance; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (!segments.ContainsKey(pos))
                {
                    segments[pos] = Instantiate(segmentPrefab, new Vector3(x * segmentSize, y * segmentSize, 0), Quaternion.identity);
                }
            }
        }

        List<Vector2Int> segmentsToRemove = new List<Vector2Int>();
        foreach (var segment in segments)
        {
            if (Vector2Int.Distance(segment.Key, playerPosition) > viewDistance)
            {
                segmentsToRemove.Add(segment.Key);
            }
        }

        foreach (var pos in segmentsToRemove)
        {
            Destroy(segments[pos]);
            segments.Remove(pos);
        }
    }
}