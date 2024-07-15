using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject mediumAsteroidPrefab;
    public GameObject smallAsteroidPrefab;
    public int size = 3; // 3 - large, 2 - medium, 1 - small

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // Destroy the bullet
            if (size == 3)
            {
                SpawnMediumAsteroids(2);
            }
            else if (size == 2)
            {
                SpawnSmallAsteroids(2);
            }
            Destroy(gameObject); 
        }
    }

    void SpawnMediumAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(mediumAsteroidPrefab, transform.position, Quaternion.identity);
        }
    }

    void SpawnSmallAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity);
        }
    }
}
