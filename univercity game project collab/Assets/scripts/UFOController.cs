using UnityEngine;

public class UFOController : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Transform player;
    public float fireInterval = 1.5f;
    public float speed = 2f;

    private void Start()
    {
        InvokeRepeating("FireAtPlayer", fireInterval, fireInterval);
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Transform>();
            GetComponent<Rigidbody2D>().velocity = (player.position - transform.position).normalized * speed;
        }
        else
        {
            Debug.LogError("Player object not found! Make sure there is a GameObject with tag 'Player' in your scene.");
        }
    }

    private void FireAtPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject); 
        }
    }
}
