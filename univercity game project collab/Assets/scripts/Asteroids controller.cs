using UnityEngine;

public class Asteroidscontroller : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    int rotSpeed;
    private Vector2 movement;
    [SerializeField] private int speed = 5;

    void Start()
    {
        rotSpeed = Random.Range(-50, 50);
        rb = GetComponent<Rigidbody2D>();
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
}
