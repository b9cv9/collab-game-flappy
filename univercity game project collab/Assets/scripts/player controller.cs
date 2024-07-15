using UnityEngine;
using System.Collections;
public class playercontroller : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    public int lives = 3;

    void Start()
    {
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
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);

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

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("UFO"))
        {
            lives--;
            Debug.Log("Player hit! Lives left: " + lives);

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
}


    

