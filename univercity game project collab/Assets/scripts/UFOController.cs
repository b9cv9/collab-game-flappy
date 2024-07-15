using UnityEngine;

public class UFOController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform player;
    public float fireInterval = 1.5f;

    private void Start()
    {
        InvokeRepeating("FireAtPlayer", fireInterval, fireInterval);
    }

    private void FireAtPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * 5f;
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
