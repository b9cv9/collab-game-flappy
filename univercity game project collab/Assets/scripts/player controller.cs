using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
        // Получаем значение между -1 и 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 - влево
        vertical = Input.GetAxisRaw("Vertical"); // -1 - вниз

        // Поворот персонажа за курсором
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ - 90f);

        // Стрельба
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Проверка диагонального движения
        {
            // Ограничение скорости при движении по диагонали до 70%
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
        // Временно делаем игрока неуязвимым
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        yield return new WaitForSeconds(2); // Время неуязвимости
        collider.enabled = true;
    }

    public void minusLive()
    {
        lives--;
        if (lives <= 0)
        {
            // Игрок умирает
            Destroy(gameObject);
            Debug.Log("Player died.");
        }
        else
        {
            // Обрабатываем столкновение, отталкивание или временное бессмертие
            // Можно добавить эффект временного бессмертия или отбрасывание игрока
            StartCoroutine(TemporaryInvincibility());
        }
        textLives.text = "Жизни: " + lives.ToString();
    }


    public void addScore(int value)
    {
        score += value;
        textScore.text = "Счёт: " + score.ToString();
    }
}


    

