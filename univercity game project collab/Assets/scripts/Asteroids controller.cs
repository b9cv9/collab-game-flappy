using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject mediumAsteroidPrefab;
    public GameObject smallAsteroidPrefab;
    public int size = 3; // 3 - large, 2 - medium, 1 - small
    private Transform player;
    private Rigidbody2D rb;
    int rotSpeed;
    private Vector2 movement;
    [SerializeField] private int speed = 5;

    private void Start()
    {
        AddRandomVelocity();
        rotSpeed = UnityEngine.Random.Range(-50, 50);
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    
    
 
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
    }
    private void FixedUpdate()
    {
        MoveChar(movement);
    }
    private void MoveChar(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime * rotSpeed));
    }


    private void AddRandomVelocity()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Random.insideUnitCircle * 2f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
            if (Random.value < 0.75f)
            {
                SpawnMediumAsteroids(2); // Split into two medium asteroids
            }
            else
            {
                SpawnMediumAsteroids(0); // Split into one medium asteroid
            }
        }
        else if (size == 2)
        {
            if (Random.value < 0.75f)
            {
                SpawnSmallAsteroids(2); // Split into two small asteroids
            }
            else
            {
                SpawnSmallAsteroids(0); // Split into one small asteroid
            }
        }

        Destroy(gameObject);
    }

    void SpawnMediumAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(transform.position.x - 5f, transform.position.x + 5f), Random.Range(transform.position.y - 5f, transform.position.y + 5f), transform.position.z);
            Instantiate(mediumAsteroidPrefab, randPos, Quaternion.identity).GetComponent<AsteroidController>().size = 2;
        }
    }

    void SpawnSmallAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(transform.position.x - 5f, transform.position.x + 5f), Random.Range(transform.position.y - 5f, transform.position.y + 5f), transform.position.z);
            Instantiate(smallAsteroidPrefab, randPos, Quaternion.identity).GetComponent<AsteroidController>().size = 1;
        }
    }
}
