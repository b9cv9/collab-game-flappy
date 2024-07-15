using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject mediumAsteroidPrefab;
    public GameObject smallAsteroidPrefab;
    public int size = 3; // 3 - large, 2 - medium, 1 - small

    private void Start()
    {
        AddRandomVelocity();
    }

    private void AddRandomVelocity()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Random.insideUnitCircle * 2f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // Destroy the bullet
            HandleAsteroidDestruction();
        }
    }

    void HandleAsteroidDestruction()
    {
        if (size == 3)
        {
            if (Random.value < 0.5f)
            {
                SpawnMediumAsteroids(2); // Split into two medium asteroids
            }
            else
            {
                SpawnMediumAsteroids(1); // Split into one medium asteroid
            }
        }
        else if (size == 2)
        {
            if (Random.value < 0.5f)
            {
                SpawnSmallAsteroids(2); // Split into two small asteroids
            }
            else
            {
                SpawnSmallAsteroids(1); // Split into one small asteroid
            }
        }

        Destroy(gameObject);
    }

    void SpawnMediumAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(mediumAsteroidPrefab, transform.position, Quaternion.identity).GetComponent<AsteroidController>().size = 2;
        }
    }

    void SpawnSmallAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity).GetComponent<AsteroidController>().size = 1;
        }
    }
}
