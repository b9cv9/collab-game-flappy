using UnityEngine;

public class UFOController : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Transform player;
    public float fireInterval = 0.5f;
    public float speed = 2f;
    public float safeDistance = 5f;

    private void Start()
    {
        InvokeRepeating("FireAtPlayer", fireInterval, fireInterval);
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Transform>();
        }
        else
        {
            Debug.LogError("Player object not found! Make sure there is a GameObject with tag 'Player' in your scene.");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            float distance = Vector2.Distance(player.position, transform.position);

            if (distance > safeDistance)
            {
                GetComponent<Rigidbody2D>().velocity = direction * speed;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    private void FireAtPlayer()
    {
        if (player != null)
        {
            
            Vector2 direction = (player.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 2f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Debug.Log("UFO destroyed.");
        }
    }
   
}
