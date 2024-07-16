using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    public Text textScore, textLives;
    
    Rigidbody2D body;
    
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    
    private int score = 0;
    
    public float runSpeed = 20.0f;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    public int lives = 3;

    void Start()
    {
        textScore.text = "Счёт: " + score.ToString();
        textLives.text = "Жизни: " + lives.ToString();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ - 90f);

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) 
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        } 

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    void Shoot()
    {
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }
    
    
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("UFO") || collision.gameObject.CompareTag("ufoBullet"))
        {
            Destroy(collision.gameObject);
            minusLive();
            Debug.Log("Player hit! Lives left: " + lives);
        }
    }

    private IEnumerator TemporaryInvincibility()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        yield return new WaitForSeconds(2);
        collider.enabled = true;
    }

    public void minusLive()
    {
        lives--;
        if (lives <= 0)
        {
            lives = 0;
            Destroy(gameObject);
            Debug.Log("Player died.");
            SceneManager.LoadScene(5);
        }
        else
        {

            StartCoroutine(TemporaryInvincibility());
        }
        textLives.text = "Жизни: " + lives.ToString();
    }


    public void addScore(int value)
    {
        ScoreManager.instance.AddScore(value);
        textScore.text = "Счёт: " + ScoreManager.instance.GetScore().ToString();
    }
}


    

