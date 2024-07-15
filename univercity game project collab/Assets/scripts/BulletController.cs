using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float timeDestroy = 3f;
    public float speed = 10f;
    private Rigidbody2D rb;
    private playercontroller _pc;

    void Start()
    {
        _pc = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontroller>();
        rb = GetComponent<Rigidbody2D>();
        if (gameObject.CompareTag("Bullet"))
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            rb.velocity = transform.right * speed;
            Invoke("DestroyBullet", timeDestroy);
        }
        else
        {
            Vector3 difference = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position - transform.position;
            difference.Normalize();
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            rb.velocity = transform.right * speed;
            Invoke("DestroyBullet", timeDestroy);
        }
    }

    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("ufoBullet"))
        {
            _pc.minusLive();
            Destroy(gameObject);
        }
    }
}