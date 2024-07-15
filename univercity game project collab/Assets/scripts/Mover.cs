using UnityEngine;

public class Mover : MonoBehaviour
{
    private Transform player;
    public float speed = 2f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        GetComponent<Rigidbody2D>().velocity = (player.position - transform.position).normalized * speed;
    }
}
